using ForumApp.DB;
using ForumApp.Entity;
using ForumApp.Models;
using ForumApp.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ForumApp.Controllers
{
    public class BaseController : Controller
    {
        ForumDbContext context = new ForumDbContext();
        // GET: Base
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 登陆页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 登录检查
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public JsonResult LoginCheck(UserInfo info)
        {
            ResultMode result = new ResultMode();
            try
            {
                var model = context.UserInfos.Where(m => m.nickName == info.nickName && m.password == info.password).FirstOrDefault();
                if (model == null)
                {
                    result.Code = "-1";
                    result.Msg = "用户名或密码有误!";
                }
                else
                {
                    result.Code = "1";
                    result.Msg = "登录成功";
                }
            }
            catch (Exception e)
            {
                result.Code = "-1";
                result.Msg = e.Message;
            }
            return Json(result);
        }
        /// <summary>
        /// 注册页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <returns></returns>
        public ActionResult AddUser(UserInfo info)
        {
            var model = context.UserInfos.Where(m => m.id == info.id).FirstOrDefault();
            if (model == null)
            {
                model = new UserInfo();
            }
            return View(model);
        }
        /// <summary>
        /// 用户详情
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult Detail(UserInfo info)
        {
            var model = context.UserInfos.Where(m => m.id == info.id).FirstOrDefault();
            if (model == null)
            {
                model = new UserInfo();
            }
            return View(model);
        }

        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public JsonResult RegisterCheck(UserInfo info)
        {
            ResultMode result = new ResultMode();
            try
            {
                using (var context = new ForumDbContext())
                {
                    #region 新增
                    var model = context.UserInfos.Where(m => m.id == info.id).FirstOrDefault();
                    if (model == null)
                    {
                        var userinfo = context.UserInfos.Where(m => m.nickName == info.nickName).FirstOrDefault();
                        if (userinfo != null)
                        {
                            result.Code = "-1";
                            result.Msg = "当前用户：'" + userinfo.nickName + "'已存在";
                            return Json(result);
                        }
                        info.createTime = DateTime.Now;
                        RoleInfo role = context.RoleInfos.Where(m => m.id == 1).FirstOrDefault();
                        info.role = role;
                        info.lastLoginTime = DateTime.Now;
                        info.id = Guid.NewGuid();
                        context.UserInfos.Add(info);
                        context.SaveChanges();
                        result.Code = "1";
                        result.Msg = "注册成功";
                    }
                    else
                    {
                        model.lastLoginTime = DateTime.Now;
                        model.nickName = info.nickName;
                        model.password = info.password;
                        model.phone = info.phone;
                        model.email = info.email;
                        model.headPicture = info.headPicture;
                        model.sex = info.sex;
                        context.SaveChanges();
                        result.Code = "1";
                        result.Msg = "修改成功";
                    }
                    #endregion
                    //var model = context.UserInfos.Where(m => m.nickName == info.nickName).FirstOrDefault();
                    
                    
                }
            }
            catch (Exception ex)
            {
                result.Code = "-1";
                result.Msg = "注册失败--"+ ex.Message;
            }
            return Json(result);
        }
        
        public ActionResult test()
        {
            return View();
        }

        public JsonResult GetUserList(int page,int limit)
        {
            TableModel<UserModel> model = new TableModel<UserModel>();
            try
            {
                model = GetList(page,limit);
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message);
                throw e;
            }
            
        }
        /// <summary>
        /// 获取用户数据列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public TableModel<UserModel> GetList(int pageIndex,int pageSize)
        {
            TableModel<UserModel> model = new TableModel<UserModel>();
            var userinfo = context.UserInfos;
            var roleinfo = context.RoleInfos;
            var list = from ui in userinfo
                       join ri in roleinfo
                       on ui.role.id equals ri.id
                       select new UserModel()
                       {
                           id = ui.id,
                           nickName = ui.nickName,
                           email = ui.email,
                           phone = ui.phone,
                           createTime = ui.createTime,
                           lastLoginTime = ui.lastLoginTime,
                           sex = ui.sex == 1 ? "男" : "女",
                           role = ri.type
                       };
            var count = userinfo.Count();
            model.code = "0";
            model.msg = "";
            model.count = count;
            model.data = list.OrderBy(m=>m.createTime).Skip((pageIndex-1)*pageSize).Take(pageSize).ToList();
            return model;
        }
        /// <summary>
        /// 上传头像
        /// </summary>
        /// <returns></returns>
        public ActionResult Upload()
        {
            #region + 变量
            //文件大小（字节数）
            long fileSize = 0;
            //重命名的文件名称
            string tempName = "";
            //物理路径+重命名的文件名称
            string fileName = "";
            //文件扩展名
            string extname = "";
            //虚拟路径
            string virtualPath = "~/HeadPicture";
            //回传到前端的路径
            string returnPath = "/HeadPicture/";
            //本地存储路径
            string path = Server.MapPath(virtualPath);
            //获取提交的文件
            var file = Request.Files[0];
            ResultMode result = new ResultMode();
            #endregion
            #region 将前端传过来的文件下载到本地
            try
            {
                //判断前端是否有传递文件过来
                if (file != null && !string.IsNullOrEmpty(file.FileName))
                {
                    //判断本地是否存在NewsCoverImage文件夹，如果没有就创建
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    //获取文件的后缀
                    extname = file.FileName.Substring(file.FileName.LastIndexOf('.'), (file.FileName.Length - file.FileName.LastIndexOf('.')));
                    //文件重新命名
                    tempName = Guid.NewGuid().ToString() + extname;
                    //构建文件存储路径
                    fileName = path + "\\" + tempName;
                    returnPath = returnPath + tempName;
                    //获取文件的长度
                    fileSize += file.ContentLength;
                    //使用FileStream读取文件
                    using (FileStream fs = System.IO.File.Create(fileName))
                    {
                        file.InputStream.CopyTo(fs);
                        fs.Flush();
                    }
                    result.Code = "1";
                    result.Msg = "图片上传成功";
                    result.Data = returnPath;
                }
            }
            catch (Exception ex)
            {
                result.Code = "-1";
                result.Msg = "图片上传失败" + ex.Message;
                return Json(result);
            }
            #endregion
            return Json(result);
        }
        /// <summary>
        /// 删除指定用户
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public ActionResult DeleteUser(UserInfo info)
        {
            ResultMode result = new ResultMode();
            try
            {
                using (var context = new ForumDbContext())
                {
                    var model = context.UserInfos.Where(m => m.id == info.id).FirstOrDefault();
                    if (model == null)
                    {
                        result.Code = "-1";
                        result.Msg = "数据不存在，数据删除失败";
                    }
                    else
                    {
                        context.UserInfos.Remove(model);
                        context.SaveChanges();
                        result.Code = "1";
                        result.Msg = "数据删除成功";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Code = "1";
                result.Msg = "数据删除失败，"+ex.Message;
            }
            return Json(result);
        }
    }
}