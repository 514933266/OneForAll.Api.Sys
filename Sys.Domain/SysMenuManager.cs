using AutoMapper;
using Sys.Domain.AggregateRoots;
using Sys.Domain.Interfaces;
using Sys.Domain.Models;
using Sys.Domain.Repositorys;
using OneForAll.Core;
using OneForAll.Core.DDD;
using OneForAll.Core.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sys.Domain.Aggregates;
using System.Collections.Immutable;
using OneForAll.EFCore;
using Sys.Domain.Enums;
using System.Collections;

namespace Sys.Domain
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class SysMenuManager : BaseManager, ISysMenuManager
    {
        private readonly IMapper _mapper;
        private readonly ISysMenuRepository _repository;
        private readonly ISysPermissionRepository _permRepository;
        public SysMenuManager(
            IMapper mapper,
            ISysMenuRepository repository,
            ISysPermissionRepository permRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _permRepository = permRepository;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="hasPerms">是否包含权限</param>
        /// <param name="parentId">上级节点</param>
        /// <param name="key">关键字</param>
        /// <returns>列表</returns>
        public async Task<IEnumerable<SysMenuPermissionAggr>> GetListAsync(bool hasPerms, Guid parentId, string key)
        {
            var data = await _repository.GetListAsync(parentId, key);
            var result = _mapper.Map<IEnumerable<SysMenu>, IEnumerable<SysMenuPermissionAggr>>(data);
            if (hasPerms)
            {
                // 查询带权限时，只显示启用的菜单
                result = result.Where(w => w.IsEnabled).ToList();
                var ids = data.Select(s => s.Id).ToList();
                var perms = await _permRepository.GetListByMenuAsync(ids);
                result.ForEach(e =>
                {
                    e.SysPermissions = perms.Where(w => w.SysMenuId == e.Id).OrderByDescending(o => o.SortCode).ToList();
                });
            }
            return result;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="entity">菜单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> AddAsync(SysMenuForm entity)
        {
            var exists = await CheckParentExists(entity);
            if (!exists) return BaseErrType.DataNotFound;

            var data = _mapper.Map<SysMenuForm, SysMenu>(entity);

            using (var tran = new UnitOfWork().BeginTransaction())
            {
                await _repository.AddAsync(data, tran);
                await _permRepository.AddAsync(new SysPermission()
                {
                    Id = Guid.Empty,
                    SysMenuId = data.Id,
                    Name = "查看页面",
                    Code = "EnterView",
                    SortCode = "0000",
                    Remark = "可以查看页面"
                }, tran);
                return await ResultAsync(tran.CommitAsync);
            }
        }

        /// <summary>
        /// 克隆
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="mids">克隆id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> CopyAsync(Guid id, IEnumerable<Guid> mids)
        {
            var data = await _repository.FindAsync(id);
            if (data == null)
                return BaseErrType.DataNotFound;
            if (!mids.Any())
                return BaseErrType.DataEmpty;
            var menus = await _repository.GetListAsync(mids);
            if (!menus.Any())
                return BaseErrType.DataEmpty;

            var treeNodes = _mapper.Map<IEnumerable<SysMenu>, IEnumerable<SysMenuTreeAggr>>(menus);
            var perms = await _permRepository.GetListByMenuAsync(mids);

            var newMenus = new List<SysMenu>();
            var newPerms = new List<SysPermission>();
            Copy(id, treeNodes, perms, newMenus, newPerms);

            if (newMenus.Any())
            {
                using (var tran = new UnitOfWork().BeginTransaction())
                {
                    await _repository.AddRangeAsync(newMenus, tran);
                    await _permRepository.AddRangeAsync(newPerms, tran);
                    return await ResultAsync(tran.CommitAsync);
                }
            }
            return BaseErrType.Fail;
        }

        public void Copy(Guid parentId, IEnumerable<SysMenu> nodes, IEnumerable<SysPermission> perms, List<SysMenu> newMenus, List<SysPermission> newPerms)
        {

            nodes.ForEach(e =>
            {
                var curPerms = perms.Where(w => w.SysMenuId == e.Id).ToList();

                e.Id = Guid.NewGuid();
                e.ParentId = parentId;
                curPerms.ForEach(perm => { perm.Id = Guid.Empty; perm.SysMenuId = e.Id; });
                newMenus.Add(e);
                newPerms.AddRange(curPerms);
            });
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">菜单</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> UpdateAsync(SysMenuForm entity)
        {
            var exists = await CheckParentExists(entity);
            if (!exists)
                return BaseErrType.DataNotFound;
            var data = await _repository.FindAsync(entity.Id);
            if (data == null)
                return BaseErrType.DataNotFound;

            _mapper.Map(entity, data);
            return await ResultAsync(() => _repository.UpdateAsync(data));
        }

        private async Task<bool> CheckParentExists(SysMenuForm entity)
        {
            if (entity.ParentId != Guid.Empty)
            {
                var menus = await _repository.GetListAsync();
                var parent = menus.FirstOrDefault(w => w.Id == entity.ParentId);
                var children = menus.FindChildren(entity.Id);

                // 1. 禁止选择不存在的上级
                // 2. 禁止选择下级作为自己的上级
                if (parent == null) return false;
                if (entity.Id != Guid.Empty &&
                    children.Any(w => w.Id.Equals(entity.ParentId))) return false;
            }
            return true;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="ids">菜单id</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> DeleteAsync(IEnumerable<Guid> ids)
        {
            var canDel = true;
            var errType = BaseErrType.Success;
            var data = await _repository.GetListAsync();
            var menus = data.Where(w => ids.Contains(w.Id)).ToList();
            if (data.Count() < 1 || menus.Count < 1) return BaseErrType.DataEmpty;

            for (var i = 0; i < menus.Count; i++)
            {
                var element = menus[i];
                if (element.IsDefault)
                {
                    canDel = false;
                    errType = BaseErrType.NotAllow;
                    break;
                }
                else
                {
                    var children = data.FindChildren(element.Id);
                    if (children.Count() > 0)
                    {
                        canDel = false;
                        errType = BaseErrType.DataExist;
                        break;
                    }
                }
            }
            if (canDel)
            {
                errType = await ResultAsync(() => _repository.DeleteRangeAsync(menus));
            }
            return errType;
        }

        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="id">菜单</param>
        /// <param name="isEnable">是否启用</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> EnableAsync(Guid id, bool isEnable)
        {
            var data = await _repository.FindAsync(id);
            if (data == null)
                return BaseErrType.DataNotFound;

            data.SetEnable();

            return await ResultAsync(() => _repository.UpdateAsync(data));
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="id">实体id</param>
        /// <param name="sortNumber">序号</param>
        /// <returns>结果</returns>
        public async Task<BaseErrType> SortAsync(Guid id, int sortNumber)
        {
            var data = await _repository.FindAsync(id);
            if (data == null)
                return BaseErrType.DataNotMatch;
            var items = await _repository.GetListAsync(w => w.ParentId == data.ParentId);
            if (!items.Any())
                return BaseErrType.DataEmpty;

            var total = items.Count();
            items = items.OrderBy(o => o.SortNumber).ToList();
            var dic = new Dictionary<SysMenu, int>();
            for (var i = 0; i < total; i++)
            {
                dic.Add(items.ElementAt(i), i);
            }

            var index = dic.First(w => w.Key.Id == id).Value;
            foreach (var kv in dic)
            {
                if (sortNumber > 0)
                {
                    if (kv.Value == index - 1)
                        dic[kv.Key] = index;
                    else if (kv.Value == index)
                        dic[kv.Key] = index - 1;
                }
                else
                {
                    if (kv.Value == index + 1)
                        dic[kv.Key] = index;
                    else if (kv.Value == index)
                        dic[kv.Key] = index + 1;
                }
            };

            items.ForEach(e =>
            {
                e.SortNumber = dic[e];
            });

            return await ResultAsync(_repository.SaveChangesAsync);
        }
    }
}
