using System.ComponentModel.DataAnnotations;

namespace HotelProject.WebUI.Dtos.RegisterDto
{
    public class CreateNewUserDto
    {
        [Required(ErrorMessage ="İsim kısmı boş bırakılamaz.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Soyisim kısmı boş bırakılamaz.")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Kullanıcı adı kısmı boş bırakılamaz.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mail kısmı boş bırakılamaz.")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Şifre kısmı boş bırakılamaz.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Şifre kısmı boş bırakılamaz.")]
        [Compare("Password",ErrorMessage ="Şifreler uyuşmuyor.")]
        public string ConfirmPassword { get; set; }
		public int WorkLocationID { get; set; }
	}
}
