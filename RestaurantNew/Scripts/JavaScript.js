function check()
{
    if (Session["User"] != null) {
        if (document.getElementById("defaultUnchecked").checked == true) {

            ((RestaurantNew.Models.ApplicationUser)Session["User"]).PhoneNumber = "none";
            alert((RestaurantNew.Models.ApplicationUser)Session["User"]).PhoneNumber );
        }
        else if (document.getElementById("defaultUnchecked").checked == true) {

            ((RestaurantNew.Models.ApplicationUser)Session["User"]).PhoneNumber = "solder";
            alert((RestaurantNew.Models.ApplicationUser)Session["User"]).PhoneNumber );
        }
        else if (document.getElementById("defaultUnchecked").checked == true) {

            ((RestaurantNew.Models.ApplicationUser)Session["User"]).PhoneNumber = "student";
            alert((RestaurantNew.Models.ApplicationUser)Session["User"]).PhoneNumber );
        }
        else if (document.getElementById("defaultUnchecked").checked == true) {

            ((RestaurantNew.Models.ApplicationUser)Session["User"]).PhoneNumber = "manager";
            alert((RestaurantNew.Models.ApplicationUser)Session["User"]).PhoneNumber );
        }

    }
}