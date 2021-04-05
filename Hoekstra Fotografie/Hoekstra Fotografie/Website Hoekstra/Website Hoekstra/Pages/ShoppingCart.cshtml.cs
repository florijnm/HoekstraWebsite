using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Website_Hoekstra.Pages
{
    public class ShoppingCartModel : PageModel
    {
        public List<PhotosOrdered> PhotosOrder { get; set; }

        public void OnGet()
        {
        }

        [BindProperty] public PhotosOrdered photos { get; set; } = new PhotosOrdered();
        [BindProperty] public login_user user { get; set; }
        [BindProperty] public user_controller IdUser { get; set; }

        public void OnPostAddPhotoOrder(int picture_id)
        {
            photos.picture_id = picture_id;
            int SessionCookie = 3 ; //User_id gekregen van cookie
            IdUser.user_id = SessionCookie;
            photos.order_id = SessionCookie;
            if (SessionCookie > 0)
            {
                PhotosOrder = SessionExtensions.Get<List<PhotosOrdered>>(HttpContext.Session, "PhotosOrder");
                if (PhotosOrder == null)
                {
                    new DBRepos().NewOrder(IdUser, photos);
                    PhotosOrder = new List<PhotosOrdered>();
                    PhotosOrder.Add(photos);
                    new DBRepos().PhotoOrder(photos);
                    SessionExtensions.Set(HttpContext.Session, "PhotosOrder", PhotosOrder);
                }
                else
                {
                    int index = Exists(PhotosOrder, picture_id);
                    if (index == -1)
                    {
                        PhotosOrder.Add(photos);
                        new DBRepos().PhotoOrder(photos);
                    }
                    else
                    {
                        throw new NotImplementedException(); //Waarschuwing geven van dat dit product al in winkelwagen zit
                    }
                    SessionExtensions.Set(HttpContext.Session, "PhotosOrder", PhotosOrder);
                }
            }
            else
            {
                throw new NotImplementedException(); //Hier moet een waarschuwing komen dat er nog niet ingelogd is
            }
        }

        public int Exists(List<PhotosOrdered> photos, int id)
        {
            for (int i = 0; i < photos.Count; i++)
            {
                if (photos[i].picture_id == id)
                {
                    return i;
                }
            }
            return -1;
        }


        public int GetPrice()
        {
            return new DBRepos().PricePhoto(photos);
        }

       
    }
}
