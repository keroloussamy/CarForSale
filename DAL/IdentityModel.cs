using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ApplicationUserIdentity : IdentityUser
    {
        public virtual Address Address { get; set; }
    }

    public class ApplicationUserStore : UserStore<ApplicationUserIdentity>
    {
        public ApplicationUserStore() : base(new ApplicationDBContext())
        {

        }
        public ApplicationUserStore(DbContext db) : base(db)
        {

        }
    }

    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager()
            : base(new RoleStore<IdentityRole>(new ApplicationDBContext()))
        {

        }
        public ApplicationRoleManager(DbContext db)
            : base(new RoleStore<IdentityRole>(db))
        {

        }
    }


    public class ApplicationUserManager : UserManager<ApplicationUserIdentity>
    {
        public ApplicationUserManager() : base(new ApplicationUserStore())
        {

        }
        public ApplicationUserManager(DbContext db) : base(new ApplicationUserStore(db))
        {

        }
    }


    /*=========================== Models ========================*/
    public enum Color
    {
        Pink, Red, Maroon, Brown, Misty, Rose, Salmon, Coral, OrangeRed, Chocolate, Orange, Gold, Ivory, Yellow, Olive, YellowGreen, LawnGreen, Chartreuse,
        Lime, Green, SpringGreen, Aquamarine
    }
    public enum Condition
    {
        New,
        Used
    }
    public class Car
    {
        public int Id { get; set; }

        [Required]
        public string Model { get; set; }
        public int Mileage { get; set; }
        public int Price { get; set; }
        [Required]
        public string Engine { get; set; }
        [Required]
        public Color Color { get; set; }
        [Required]
        public Condition Condition { get; set; }
        [Required]
        public string Image { get; set; }

        public int Year { get; set; }

        [ForeignKey("Dealer")]
        [Required]
        public string DealerId { get; set; }
        public virtual Dealer Dealer { get; set; }

        [ForeignKey("Brand")]
        public int BrandId { get; set; }
        public virtual Brand Brand { get; set; }
    }
    public class Brand
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }


        public virtual ICollection<Car> Cars { get; set; }

    }

    public class Dealer
    {
        [Key, ForeignKey("User")]
        public string Id { get; set; }

        public virtual ApplicationUserIdentity User { get; set; }

        public virtual ICollection<Message> Massages { get; set; }
        public virtual ICollection<Car> Cars { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
    public enum City
    {
        Alexandria,
        Aswan,
        Asyut,
        Beheira,
        BeniSuef,
        Cairo,
        Dakahlia,
        Damietta,
        Faiyum,
        Gharbia,
        Giza,
        Ismailia,
        KafrElSheikh,
        Luxor,
        Matruh,
        Minya,
        Monufia,
        NewValley,
        NorthSinai,
        PortSaid,
        Qalyubia,
        Qena,
        RedSea,
        Sharqia,
        Sohag,
        SouthSinai,
        Suez
    }
    public class Address
    {
        [Key, ForeignKey("User")]
        public string Id { get; set; }
        [Required]
        public City City { get; set; }
        [Required]
        public string Street { get; set; }
        public string Direction { get; set; }

        public virtual ApplicationUserIdentity User { get; set; }
    }
    public class Message
    {
        public int Id { get; set; }

        [Required]
        public string body { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [ForeignKey("Dealer")]
        [Required]
        public string DealerId { get; set; }
        public virtual Dealer Dealer { get; set; }

    }

    public class Appointment
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string CustomerName { get; set; }
        [Required]
        public string CustomerPhoneNumber { get; set; }
        [Required]
        public string CustomerEmail { get; set; }

        [ForeignKey("Dealer")]
        [Required]
        public string DealerId { get; set; }
        public virtual Dealer Dealer { get; set; }

        [ForeignKey("Employee")]
        public string EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

    }
    public class Employee
    {
        [Key, ForeignKey("User")]
        public string Id { get; set; }

        public virtual ApplicationUserIdentity User { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }

        [ForeignKey("Dealer")]
        [Required]
        public string DealerId { get; set; }
        public virtual Dealer Dealer { get; set; }
    }

    /*=========================== End of Models ========================*/

    public class ApplicationDBContext : IdentityDbContext<ApplicationUserIdentity>
    {

        public ApplicationDBContext() :
            base("name=CS")
        {

        }
        public virtual DbSet<Car> Cars { get; set; }
        public virtual DbSet<Dealer> Dealers { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Address> Addresses { get; set; }
    }
}
