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
    [RoutePrefix("api/Article")]
    public class ArticleController : ApiController
    {
        IArticleAccessing _accessing;

        public ArticleController(IArticleAccessing accessing)
        {
            _accessing = accessing;
        }

        [HttpGet]
        public IEnumerable<Article> GetArticlesOfCategory(int id)
        {
            return _accessing.GetByCategoryId(id);
        }

        [HttpGet]
        public Article GetArticle(int id)
        {
            return _accessing.GetById(id);
        }

        [HttpPost]
        public bool AddArticle([FromBody]Article article, [FromBody] HttpPostedFileBase video, [FromBody] HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                _accessing.Add(article);

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
                        _accessing.Delete(article);
                        return false;
                    }
                }
                else
                {
                    _accessing.Delete(article);
                    return false;
                }

            }
            else
            {
                return false;
            }
        }

        [HttpPut]
        public bool AddArticle(int id, [FromBody]Article article, [FromBody] HttpPostedFileBase video, [FromBody] HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                _accessing.Add(article);
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
                        _accessing.Delete(article);
                        return false;
                    }
                }
                else
                {
                    _accessing.Delete(article);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        [HttpPut]
        public bool EditArticle(int id, [FromBody]Article article)
        {
            if (ModelState.IsValid)
            {
                _accessing.Edit(article);

                return true;
            }
            else
            {
                return false;
            }
        }

        [HttpDelete]
        public bool DeleteArticle([FromBody]Article article)
        {
            _accessing.Delete(article);

            return true;
        }
    }
}
