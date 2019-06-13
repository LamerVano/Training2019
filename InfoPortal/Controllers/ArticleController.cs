using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Common;
using BuisnesLogic;
using Unity;
using System.Web;

namespace InfoPortal.Controllers
{
    public class ArticleController : ApiController
    {
        IArticleAccessing _accessing;

        public ArticleController(IArticleAccessing accessing)
        {
            _accessing = accessing;
        }

        // GET api/article/5
        public Article Get(int id)
        {
            return _accessing.GetArticle(id);
        }

        public bool Post([FromBody]Article article, [FromBody] HttpPostedFileBase video, [FromBody] HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if(_accessing.AddArticle(article))
                {
                    if (video != null)
                    {
                        string path = "~/Content/Articles/" + article.Id;

                        string videoType = "." + video.FileName.Split('.')[1];
                        string imgType = "." + image.FileName.Split('.')[1];

                        article.Video = path + videoType;
                        article.Picture = path + imgType;

                        try
                        {
                            video.SaveAs(article.Video);
                            image.SaveAs(article.Picture);
                            return true;
                        }
                        catch
                        {
                            _accessing.DelArticle(article.Id);
                            return false;
                        }
                    }
                    else
                    {
                        _accessing.DelArticle(article.Id);
                        return false;
                    }                    
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        // PUT api/article/5
        //[Authorize(Roles = Roles.EDITOR)]
        public bool Put(int id, [FromBody]Article article, [FromBody] HttpPostedFileBase video, [FromBody] HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (_accessing.AddArticle(article))
                {
                    if (video != null)
                    {
                        try
                        {
                            video.SaveAs(article.Video);
                            image.SaveAs(article.Picture);

                            return true;
                        }
                        catch
                        {
                            _accessing.DelArticle(article.Id);
                            return false;
                        }
                    }
                    else
                    {
                        _accessing.DelArticle(article.Id);
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool Put(int id, [FromBody]Article article)
        {
            if (ModelState.IsValid)
            {
                return _accessing.UpdateArticle(article);
            }
            else
            {
                return false;
            }
        }

        // DELETE api/article/5
        //[Authorize(Roles = Roles.EDITOR)]
        public bool Delete(int id)
        {
            return _accessing.DelArticle(id);
        }
    }
}
