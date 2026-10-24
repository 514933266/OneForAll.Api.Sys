using AutoMapper;
using OneForAll.Core;
using OneForAll.Core.Events;
using OneForAll.Core.Extension;
using OneForAll.Core.UploadFile;
using OneForAll.EFCore;
using OneForAll.File;
using Sys.Domain.Entities;
using Sys.Domain.Enums;
using Sys.Domain.Events;
using Sys.Domain.Interfaces;
using Sys.Domain.Models;
using Sys.Domain.Repositorys;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Domain.Managers
{
    /// <summary>
    /// 系统通知（命令：增删改/上传/发布）
    /// </summary>
    public class SysNotificationManager : SysDomainManager, ISysNotificationManager
    {
        // 文件存储路径
        private readonly string UPLOAD_PATH = "upload/notificatio/{0}";
        // 虚拟路径：根据Startup配置设置,返回给前端访问资源
        private readonly string VIRTUAL_PATH = "resources/notificatio/{0}";

        private readonly IMapper _mapper;
        private readonly IUploader _uploader;
        private readonly ISysNotificationRepository _repository;
        private readonly ISysNotificationToAccountRepository _accountRepository;
        private readonly IEventDispatcher _eventDispatcher;

        public SysNotificationManager(
            IMapper mapper,
            ICurrentUser currentUser,
            IUploader uploader,
            ISysNotificationRepository repository,
            ISysNotificationToAccountRepository accountRepository,
            IEventDispatcher eventDispatcher) : base(currentUser)
        {
            _mapper = mapper;
            _uploader = uploader;
            _repository = repository;
            _accountRepository = accountRepository;
            _eventDispatcher = eventDispatcher;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(SysNotificationForm form)
        {
            var data = _mapper.Map<SysNotificationForm, SysNotification>(form);
            if (form.Id == Guid.Empty) data.Id = Guid.NewGuid();

            if (form.Type == SysNotificationTypeEnum.AnyAccount)
            {
                using (var tran = new UnitOfWork().BeginTransaction())
                {
                    _repository.Add(data, tran);
                    form.ToAccounts.ForEach(e =>
                    {
                        _accountRepository.Add(new SysNotificationToAccount()
                        {
                            MessageId = data.Id,
                            UserId = e.Id,
                            TenantId = e.TenantId,
                            UserName = e.UserName
                        }, tran);
                    });
                    return Result(() => tran.Commit());
                }
            }
            else
            {
                return await ResultAsync(() => _repository.AddAsync(data));
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="form">表单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(SysNotificationForm form)
        {
            var data = await _repository.FindAsync(form.Id);
            if (data == null) return BaseErrType.DataNotFound;

            data.Title = form.Title;
            data.Content = form.Content;
            data.ResetToDraft();

            using (var tran = new UnitOfWork().BeginTransaction())
            {
                if (data.Type == SysNotificationTypeEnum.AnyAccount)
                {
                    var accounts = await _accountRepository.GetListAsTrackingAsync(data.Id);
                    accounts.ForEach(e =>
                    {
                        _accountRepository.Delete(e, tran);
                    });
                }

                form.ToAccounts.ForEach(e =>
                {
                    _accountRepository.Add(new SysNotificationToAccount()
                    {
                        MessageId = data.Id,
                        UserId = e.Id,
                        TenantId = e.TenantId,
                        UserName = e.UserName
                    }, tran);
                });

                data.Type = form.Type;
                _repository.Update(data, tran);

                return Result(() => tran.Commit());
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">权限id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            if (ids.Count() < 1) return BaseErrType.DataEmpty;
            var data = await _repository.GetListAsTrackingAsync(ids);
            if (data.Count() < 1) return BaseErrType.DataEmpty;

            return await ResultAsync(() => _repository.DeleteRangeAsync(data));
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="filename">文件名</param>
        /// <param name="file">文件流</param>
        /// <returns>结果</returns>
        public async Task<IUploadResult> UploadImageAsync(Guid id, string filename, Stream file)
        {
            var maxSize = 2 * 1024 * 1024;
            return await UploadAsync(id, filename, file, maxSize);
        }

        private async Task<IUploadResult> UploadAsync(Guid id, string filename, Stream file, int maxSize)
        {
            var result = new UploadResult() { Original = filename, Title = filename };
            var data = await _repository.FindAsync(id);
            if (data == null)
            {
                data = new SysNotification();
                data.Id = id;
            }
            if (new ValidateImageType().Validate(filename, file))
            {
                result = await _uploader.WriteAsync(file, UPLOAD_PATH.Fmt(id), filename, maxSize) as UploadResult;
                // 设置返回虚拟路径
                if (result.State.Equals(UploadEnum.Success))
                {
                    result.Url = Path.Combine(VIRTUAL_PATH.Fmt(id), result.Url);
                }
            }
            else
            {
                result.State = UploadEnum.TypeError;
            }
            return result;
        }


        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="id">实体id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> PublishAsync(Guid id)
        {
            var data = await _repository.FindAsync(id);
            if (data == null) return BaseErrType.DataNotFound;

            // 聚合行为：发布状态机
            var err = data.Publish();
            if (err != BaseErrType.Success) return err;

            var result = await ResultAsync(() => _repository.UpdateAsync(data));
            // 单命令语义：发布成功后由 Manager 发布领域事件
            if (result == BaseErrType.Success)
                await _eventDispatcher.PublishAsync(new NotificationPublishedEvent(data.Id, data.Title));
            return result;
        }
    }
}
