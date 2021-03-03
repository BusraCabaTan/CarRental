using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constans
{
    public static class Messages
    {
        //Rental Messages
        public static string RentalAded = "Araç Kiralandı.";
        public static string RentalNotAded = "Araç Kiralanmaya Uygun Değildir.";
        public static string RentalDeleted = "Kira Kaydı Silindi";
        public static string RentalUpdated = "Kira Kaydı Güncellendi";
        public static string RentalsListed = "Kira Kayıtları Listelendi.";
        public static string RentalListed = "Kira Kaydı Listelendi.";
        public static string RentalNotFound = "Kira Kaydı Bulunamadı.";


        //User Messages
        public static string UserAded = "Kullanıcı Eklendi.";
        public static string UserNotAded = "Kullanıcı Eklenemedi.";
        public static string UserDeleted = "Kullanıcı Silindi";
        public static string UserUpdated = "Kullanıcı Güncellendi";
        public static string UsersListed = "Kullanıcılar Listelendi.";
        public static string UserListed = "Kullanıcı Listelendi.";
        public static string UserNotFound = "Kullancı Bulunamadı.";
        public static string UserRegistered = "Kullanıcı kaydoldu.";
        public static string UserExist = "Kullanıcı Mevcut";

        //Car Messages
        public static string CarAdded = "Araba Eklendi.";
        public static string CarNotAdded = "Araba Eklenemedi.";
        public static string CarUpdated = "Araba Güncellendi.";
        public static string CarDeleted = "Araba Silindi.";
        public static string CarNotFound = "Araba Bulunamadı.";
        public static string CarListed = "Araba Listelendi.";
        public static string CarsListed = "Arabalar Listelendi.";


        //CarImage Messages
        public static string CarImageAded = "Fotoğraf Eklendi";
        public static string CarImagesLimited = "Fotoğraf Limiti Aşıldı";
        public static string CarImagesListed = "Fotoğraflar listelendi";
        public static string CarImageUpdated = "Fotoğraf güncellendi";
        public static string CarImageDeleted = "Fotoğraf silindi";

        //Brand Messages
        public static string BrandAdded = "Marka Eklendi.";
        public static string BrandNotAdded = "Marka Eklenemedi.";
        public static string BrandUpdated = "Marka Güncellendi.";
        public static string BrandDeleted = "Marka Silindi.";
        public static string BrandNotFound = "Marka Bulunamadı.";
        public static string BrandsListed= "Markalar Listelendi.";

        //Color Messages
        public static string ColorAdded = "Renk Eklendi.";
        public static string ColorNotAdded = "Renk Eklenemedi.";
        public static string ColorUpdated = "Renk Güncellendi.";
        public static string ColorDeleted = "Renk Silindi.";
        public static string ColorNotFound = "Renk Bulunamadı.";
        public static string ColorsListed = "Renkler Listelendi.";

        //Customer Messages
        public static string CustomerAdded = "Müşteri Eklendi.";
        public static string CustomerNotAdded = "Müşteri Eklenemedi.";
        public static string CustomerUpdated = "Müşteri Güncellendi.";
        public static string CustomerDeleted = "Müşteri Silindi.";
        public static string CustomerNotFound = "Müşteri Bulunamadı.";
        public static string CustomersListed = "Müşteriler Listelendi.";
        public static string CustomerListed = "Müşteri Listelendi.";


        //Authorization
        public static string AuthorizationDenied = "Yetkiniz Yok.";
        public static string AccessTokenCreated = "Token oluşturuldu";
        public static string AccessTokenNotCreated = "Token oluşturulamadı";
        public static string PasswordError = "Parola hatalı";
        public static string SuccessfulLogin = "Giriş Başarlı";
        
    }
}
