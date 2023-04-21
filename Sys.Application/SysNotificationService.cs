using AutoMapper;
using Sys.Application.Dtos;
using Sys.Application.Interfaces;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Interfaces;
using Sys.Domain.Models;
using OneForAll.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OneForAll.Core.Upload;
using System.IO;
using Sys.HttpService.Interfaces;
using Sys.Domain.Repositorys;
using Sys.Domain.Enums;
using System.Linq;
using Sys.HttpService.Models;
using Sys.HttpService.Enums;
using OneForAll.Core.Extension;

namespace Sys.Application
{
    /// <summary>
    /// 系统通知
    /// </summary>
    public class SysNotificationService : ISysNotificationService
    {
        private readonly IMapper _mapper;
        private readonly ISysNotificationManager _manager;
        private readonly IUmsMessageHttpService _messageHttpService;

        private readonly ISysUserRepository _userRepository;
        private readonly ISysNotificationRepository _repository;
        private readonly ISysNotificationToAccountRepository _toAccountRepository;
        public SysNotificationService(
            IMapper mapper,
            ISysNotificationManager manager,
            IUmsMessageHttpService messageHttpService,
            ISysUserRepository userRepository,
            ISysNotificationRepository repository,
            ISysNotificationToAccountRepository toAccountRepository)
        {
            _mapper = mapper;
            _manager = manager;
            _messageHttpService = messageHttpService;
            _userRepository = userRepository;
            _repository = repository;
            _toAccountRepository = toAccountRepository;
        }

        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页数</param>
        /// <param name="key">关键字</param>
        /// <param name="startDate">开始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns>分页列表</returns>
        public async Task<PageList<SysNotificationDto>> GetPageAsync(
            int pageIndex,
            int pageSize,
            string key,
            DateTime? startDate,
            DateTime? endDate)
        {
            var data = await _manager.GetPageAsync(pageIndex, pageSize, key, startDate, endDate);
            var items = _mapper.Map<IEnumerable<SysNotification>, IEnumerable<SysNotificationDto>>(data.Items);
            return new PageList<SysNotificationDto>(data.Total, data.PageSize, data.PageIndex, items);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(SysNotificationForm entity)
        {
            return await _manager.AddAsync(entity);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(SysNotificationForm entity)
        {
            return await _manager.UpdateAsync(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            return await _manager.DeleteAsync(ids);
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="id">文章id</param>
        /// <param name="filename">文件名</param>
        /// <param name="file">文件流</param>
        /// <returns>结果</returns>
        public async Task<IUploadResult> UploadImageAsync(Guid id, string filename, Stream file)
        {
            return await _manager.UploadImageAsync(id, filename, file);
        }

        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="id">文章id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> PublishAsync(Guid id)
        {
            var data = await _repository.FindAsync(id);
            var msg = new UmsMessageForm()
            {
                Title = data.Title,
                Content = data.Content,
                Type = UmsMessageTypeEnum.Default
            };
            if (data.Type == SysNotificationTypeEnum.AllAccount || data.Type == SysNotificationTypeEnum.MainAccount)
            {
                var pageIndex = 1;
                var pageSize = 100;
                var users = data.Type == SysNotificationTypeEnum.AllAccount ? await _userRepository.GetPageAsync(pageIndex, pageSize) : await _userRepository.GetPageMainAccountAsync(pageIndex, pageSize);
                while (users.Any())
                {
                    users.ForEach(e =>
                    {
                        msg.ToAccountId = e.Id;
                        _messageHttpService.SendAsync(msg);
                    });

                    pageIndex++;
                    users = data.Type == SysNotificationTypeEnum.AllAccount ? await _userRepository.GetPageAsync(pageIndex, pageSize) : await _userRepository.GetPageMainAccountAsync(pageIndex, pageSize);
                }
            }
            else
            {
                var toAccounts = await _toAccountRepository.GetListAsync(id);
                toAccounts.ForEach(e =>
                {
                    msg.ToAccountId = e.UserId;
                    _messageHttpService.SendAsync(msg);
                });
            }
            return await _manager.PublishAsync(id);
        }
    }
}
