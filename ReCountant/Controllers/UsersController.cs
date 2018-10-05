using ReCountant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReCountant.Controllers
{
    public class UsersController : Controller
    {
        ReCountantEntities db = new ReCountantEntities();
        // GET: Users
        public ActionResult Index()
        {
            return View();
        }
        // this will add user and user have multiple roles
        // e.g user might be supplier,employee,owner,customer
        // this will control our 4 key players.
        public ActionResult PostUser()
        {
            return View();
        }

        [HttpPost]
        public JsonResult PostUser(User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                
            }

            //var GetUserIdForEmployee = db.Users.Select(x => x.Id).ToList().LastOrDefault();
            //// Handle Employee
            //D_Employee employee = new D_Employee
            //{
            //    Userid = GetUserIdForEmployee,
            //    Name = user.Name,
            //    Employee_Info="Employee ki info"
            //};
            //db.D_Employee.Add(employee);
            //db.SaveChanges();
            //// Handle Cutomer
            //D_Customer customer = new D_Customer
            //{
            //    Userid = GetUserIdForEmployee,
            //    Name = user.Name,
            //    Customer_Info = "customer ki info"
            //};
            //db.D_Customer.Add(customer);
            //db.SaveChanges();
            //// Handle supplier
            //D_Supplier supplier = new D_Supplier
            //{
            //    Userid = GetUserIdForEmployee,
            //    Name = user.Name,
            //    Supplier_Info = "supplier ki info"
            //};
            //db.D_Supplier.Add(supplier);
            //db.SaveChanges();
            //// Handle owner
            //D_Owner owner = new D_Owner
            //{
            //    Userid = GetUserIdForEmployee,
            //    Name = user.Name,
            //    Owner_Info = "owner ki info"
            //};
            //db.D_Owner.Add(owner);
            //db.SaveChanges(); 
            return Json(user);

        }
        [HttpPost]
        public JsonResult PostUserRole(string id, string c)
        {
            
            var UserNameGet = db.Users.Where(p => p.CNIC_Number == c).Select(p => p.Name).ToList().SingleOrDefault();
            var GetUserID = db.Users.Where(p => p.CNIC_Number == c).Select(p => p.Id).ToList().SingleOrDefault();
            
           
           
           
            if (id == "employee")
            {
                var GetEmployeeID = db.D_Employee.Where(p => p.Userid == GetUserID).Select(p => p.Userid).ToList().SingleOrDefault();
                if (GetEmployeeID != GetUserID)
                {
                    D_Employee employee = new D_Employee
                    {
                        Userid = GetUserID,
                        Name = UserNameGet,
                        Employee_Info = "Employee ki info"
                    };
                   
                    if (employee != null)
                    {
                        db.D_Employee.Add(employee);
                        db.SaveChanges();
                        //return Json(employee);
                        return Json(new  { Success = true, Message = "User Added as Employee" });

                    }
                    else
                    {
                        return Json(new { Success = false, Message = "User informmation might be emplty" });
                    }
                }
                else
                {
                    return Json(false);

                }

            }
            else if (id == "customer")
            {
                var GetCustomerID = db.D_Customer.Where(p => p.Userid == GetUserID).Select(p => p.Userid).ToList().SingleOrDefault();
                if (GetCustomerID != GetUserID)
                {
                    D_Customer customer = new D_Customer
                    {
                        Userid = GetUserID,
                        Name = UserNameGet,
                        Customer_Info = "customer ki info"
                    };
                   
                    if (customer != null)
                    {
                        db.D_Customer.Add(customer);
                        db.SaveChanges();
                        return Json(new { Success = true, Message = "User Added as Customer" });
                        //return Json(customer);
                    }
                    else
                    {
                        return Json(new { Success = false, Message = "User informmation might be emplty" });
                    }
                }
                else
                {
                    return Json(false);
                }
            }
            else if (id == "owner")
            {
                var GetOwnerID = db.D_Owner.Where(p => p.Userid == GetUserID).Select(p => p.Userid).ToList().SingleOrDefault();
                if (GetOwnerID != GetUserID)
                {
                    D_Owner owner = new D_Owner
                    {
                        Userid = GetUserID,
                        Name = UserNameGet,
                        Owner_Info = "owner ki info"
                    };
                    
                    if (owner != null)
                    {
                        db.D_Owner.Add(owner);
                        db.SaveChanges();
                        //return Json(owner);
                        return Json(new { Success = true, Message = "User Added as Owner" });
                    }
                    else
                    {
                        return Json(new { Success = false, Message = "User informmation might be emplty" });
                    }
                }
                else
                {
                    return Json(false);
                }
            }
            else if (id == "supplier")
            {
                var GetSupplierID = db.D_Supplier.Where(p => p.Userid == GetUserID).Select(p => p.Userid).ToList().SingleOrDefault();
                if (GetSupplierID != GetUserID)
                {
                    D_Supplier supplier = new D_Supplier
                    {
                        Userid = GetUserID,
                        Name = UserNameGet,
                        Supplier_Info = "supplier ki info"
                    };
                   
                    if (supplier != null)
                    {
                        db.D_Supplier.Add(supplier);
                        db.SaveChanges();
                        //return Json(supplier);
                        return Json(new { Success = true, Message = "User Added as Supplier" });
                    }
                    else
                    {
                        return Json(new { Success = false, Message = "User informmation might be emplty" });
                    }
                }
                else
                {
                    return Json(false);
                }
            }
            else
            {
                return Json(false);
            }
            
        }

    }
}