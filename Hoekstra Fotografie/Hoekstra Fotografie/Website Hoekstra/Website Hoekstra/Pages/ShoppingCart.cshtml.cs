using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Website_Hoekstra.Pages
{
    public class ShoppingCartModel : PageModel
    {
        public List<PhotosOrdered> PhotosOrder { get; set; }
        float totalPrice = 0;
        public void OnGet()
        {
        }
        [BindProperty] public ValuePhoto orderdPhotos { get; set;}
        [BindProperty] public PhotosOrdered photos { get; set; } = new PhotosOrdered();
        [BindProperty] public login_user user { get; set; }
        [BindProperty] public user_controller IdUser { get; set; }

        public IActionResult OnPostAddPhotoOrder(int picture_id)
        {
            photos.picture_id = picture_id;
            int SessionCookie = Convert.ToInt32(HttpContext.Session.GetString("LoginSession"));
            IdUser.user_id = SessionCookie;
            
            if (SessionCookie > 0)
            {
                PhotosOrder = SessionExtensions.Get<List<PhotosOrdered>>(HttpContext.Session, "PhotosOrder");
                if (PhotosOrder == null)
                {
                    new DBRepos().NewOrder(IdUser);
                    photos.order_id = new DBRepos().LastOrder(IdUser);
                    PhotosOrder = new List<PhotosOrdered>();
                    PhotosOrder.Add(photos);
                    new DBRepos().PhotoOrder(photos);
                    SessionExtensions.Set(HttpContext.Session, "PhotosOrder", PhotosOrder);
                    return Page();
                }
                else
                {
                    int index = Exists(PhotosOrder, picture_id);
                    if (index == -1)
                    {
                        photos.order_id = new DBRepos().LastOrder(IdUser);
                        PhotosOrder.Add(photos);
                        new DBRepos().PhotoOrder(photos);
                        SessionExtensions.Set(HttpContext.Session, "PhotosOrder", PhotosOrder);
                        return Page();
                    }
                    else
                    {
                        return Page(); //Misschien duidelijk aangeven dat hij er al in zat
                    }
                    
                }
            }
            else
            {
                return RedirectToPage("Login");
            }
        }

        public ValuePhoto GetOrderPhoto(int picture_id)
        {
            ValuePhoto photos = new DBRepos().GetById(picture_id);
            totalPrice += photos.price; 
            
            return photos;
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


        public string GetPrice()
        {
            totalPrice = totalPrice / 4; //delen door 4 want gaat 4 keer door deze loop in de cart page
            if (ReturnDiscount(PhotosOrder) == 15)
            {
                totalPrice = totalPrice / 100 * 85; 
            }
            return totalPrice.ToString("0.00");
        }

        public int ReturnDiscount(List<PhotosOrdered> TotalPhotos)
        {
            if (TotalPhotos.Count() >= 3)
            {
                return 15;
            }
            else
            {
                return 0;
            }
        }
    }
}
