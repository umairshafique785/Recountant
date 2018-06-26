using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using ReCountant.Models;
using ReCountant.Controllers;

namespace ReContant.Controllers
{
    public class PlotManagementController : Controller
    {
        // GET: PlotManagement
        private ReCountantEntities db = new ReCountantEntities();
        public ActionResult Booking()
        {
            ViewBag.Phase = new SelectList(db.D_Phase, "Id", "Phase_Code");
            return View();
        }
        [HttpPost]
        public JsonResult GetBlocks(int id)
        {
            var res = db.D_Block.Where(x => x.Phase_Id == id).Select(x => new { Id = x.Id, Block = x.Block_Code });
            return Json(res);
        }
        [HttpPost]
        public JsonResult GetPlots(int id)
        {
            var res = db.D_Plot.Where(x => x.Block_Id == id && x.Plot_Availibilty == true).Select(x => new { Id = x.Id, Plot = x.Plot_No });
            return Json(res);
        }
        [HttpPost]
        public JsonResult GetPlotDetails(int id)
        {
            var res = (from x in db.D_Plot
                       let c = db.D_Plot_Value.Where(cl => cl.Plot_Id == x.Id).OrderByDescending(cl => cl.Id).FirstOrDefault()
                       where c != null && x.Id == id
                       select new { Id = x.Id, Length = x.Length, Plot = x.Plot_No, Area = x.Area, Width = x.Width, Front = x.Front, Value = c.Plot_Value }).SingleOrDefault();
            return Json(res);
        }
        [HttpPost]
        public JsonResult BookPlot(string data)
        {
            var res = JsonConvert.DeserializeObject<BookingInfo>(data);
            if (res.Custid == null)
            {
                UserInfo userInfo = new UserInfo
                {
                    Address = res.customer.Address,
                    Business = res.customer.Business,
                    CNIC_Number = res.customer.CNIC_Number,
                    Contact_Number = res.customer.PhoneNumber,
                    Email = res.customer.Email,
                    EPZ = res.customer.EPZ,
                    Filer_NonFiler = res.customer.Filer_NonFiler,
                    Gender = res.customer.Gender,
                    Individual_AOP_Company = res.customer.Individual_AOP_Company,
                    Industry = res.customer.Industry,
                    Name = res.customer.Name,
                    NTN = res.customer.NTN,
                    Residential_Status = res.customer.Residential_Status,
                    STRN = res.customer.STRN,
                    Supply_Chain_Role = res.customer.Supply_Chain_Role
                };
                AccountController ac = new AccountController();
                res.Custid = ac.RegisterUser(userInfo);
            }
            List<Installment_Plan> ins = res.inst.Select(z => new Installment_Plan
            {
                Due_Date = DateTime.ParseExact(z.Due_Date, "d-M-yyyy", null),
                Nth_Installment = z.Nth_Installment,
                Customer_Id = res.Custid,
                Date = DateTime.UtcNow,
                Plot_Id = z.Plot_Id,
                Status = 2,
                Rec_Revenue_Status = false,
                Amount_LC = z.Amount_LC,
                Amount_TC = z.Amount_TC,
                Currency = z.Currency,
                Exchange_Rate = z.Exchange_Rate

            }).ToList();
            db.Installment_Plan.AddRange(ins);
            db.SaveChanges();

            D_Plot_Booking d_Plot_Booking = new D_Plot_Booking()
            {
                Agreement_Date = DateTime.UtcNow,
                Customer_Id = res.Custid,
                Plot_Id = res.Plot.Plot_Id,
                Plot_Value = res.Plot.Value
            };
            db.D_Plot_Booking.Add(d_Plot_Booking);
            db.SaveChanges();

            var plot = db.D_Plot.SingleOrDefault(x => x.Id == res.Plot.Plot_Id);
            plot.Plot_Availibilty = false;
            db.D_Plot.Attach(plot);
            db.Entry(plot).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Json(true);
        }

        public ActionResult ReceiveInstallment()
        {
            return View();
        }
        public JsonResult TransactionRecorded(int Installmentid)
        {
            var res = db.Installment_Plan.SingleOrDefault(x => x.Id == Installmentid);
            res.Rec_Revenue_Status = true;
            db.Installment_Plan.Attach(res);
            db.Entry(res).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return Json(true);
        }

        public JsonResult PlotDetails(int id)
        {
            var firstres = (from x in db.D_Plot
                            join e in db.D_Block on x.Block_Id equals e.Id
                            join f in db.D_Phase on e.Phase_Id equals f.Id
                            join y in db.D_Plot_Booking on x.Id equals y.Plot_Id
                            join z in db.D_Customer on y.Customer_Id equals z.Id
                            join a in db.Installment_Plan on x.Id equals a.Plot_Id
                            let v = db.D_Plot_Value.Where(cl => cl.Plot_Id == id).OrderByDescending(cl => cl.Id).FirstOrDefault()
                            where x.Id == id
                            select new
                            {
                                Plot = new PlotDetails
                                {
                                    Id = x.Id,
                                    Area = x.Area,
                                    Block_Code = e.Block_Code,
                                    Date_Of_Sale = x.Date_Of_Sale,
                                    Front = x.Front,
                                    Length = x.Length,
                                    Phase_Code = f.Phase_Code,
                                    Plot_No = x.Plot_No,
                                    UOM = x.UOM,
                                    Width = x.Width,
                                    Plot_Value = v.Plot_Value
                                },
                                Customer = z
                            }
                      ).FirstOrDefault();
            var secres = db.Installment_Plan.Where(x => x.Plot_Id == id).ToList();
            var bal = db.Balance_Installments.Where(cl => cl.Plot_id == id).OrderByDescending(cl => cl.Id).FirstOrDefault();
            var res = new Plot_Customer_Installments_Details
            {
                Plot = firstres.Plot,
                Customer = firstres.Customer,
                Installments = secres,
                Balance = bal.Balance
            };
            return Json(res);
        }
        public ActionResult ReceiveInst(int Plotid, string Plot_Code, string Block, string Phase_Code, float Amount_TC, string Currency, float Amount_LC, float Exchange_rate, int InstId, int Custid, float Balance)
        {
            ReceiveInstallent_Details rc = new ReceiveInstallent_Details
            {
                Amount_LC = Amount_LC,
                Amount_TC = Amount_TC,
                Block = Block,
                Currency = Currency,
                Customer_Id = Custid,
                Exchange_Rate = Exchange_rate,
                Phase = Phase_Code,
                PlotCode = Plot_Code,
                Plotid = Plotid,
                Balance = Balance,
                Instid = InstId
            };
            return PartialView(rc);
        }
        public ActionResult ReceiveOtherInst(int Plotid, string Plot_Code, string Block, string Phase_Code, int Custid, float Balance)
        {
            ReceiveInstallent_Details rc = new ReceiveInstallent_Details
            {
                Block = Block,
                Customer_Id = Custid,
                Phase = Phase_Code,
                PlotCode = Plot_Code,
                Plotid = Plotid,
                Balance = Balance
            };
            return PartialView(rc);
        }

        [HttpPost]
        public JsonResult AddReceiveInstallment(Installment_Received instrec, int Istplanid)
        {
            instrec.Date = DateTime.UtcNow;
            instrec.Payment_Date = DateTime.UtcNow;
            db.Installment_Received.Add(instrec);
            db.SaveChanges();

            Mapping_Installment_Planned_Recevied mipr = new Mapping_Installment_Planned_Recevied()
            {
                InstallmentReceived_Id = instrec.Id,
                Amount = instrec.Amount_LC,
                InstallmentPlan_Id = Istplanid
            };
            db.Mapping_Installment_Planned_Recevied.Add(mipr);
            db.SaveChanges();

            Balance_Installments bal = new Balance_Installments()
            {
                Received_Amount = instrec.Amount_LC,
                Plot_id = instrec.Plot_Id,
                InstRece_Id = instrec.Id,
                Transaction_Party = instrec.Customer_Id,
                Due_Amount = 0
            };
            db.Balance_Installments.Add(bal);
            db.SaveChanges();

            var res = db.Installment_Plan.SingleOrDefault(x => x.Id == Istplanid);
            res.Status = 1;
            db.Installment_Plan.Attach(res);
            db.Entry(res).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Json(true);
        }

        [HttpPost]
        public JsonResult AddReceiveOtherInstallment(Installment_Received instrec, int Istplanid)
        {
            instrec.Date = DateTime.UtcNow;
            instrec.Payment_Date = DateTime.UtcNow;
            db.Installment_Received.Add(instrec);
            db.SaveChanges();

            var res = db.Installment_Plan.Where(x => x.Plot_Id == instrec.Plot_Id && x.Status != 1).OrderByDescending(x=>x.Id);
            double Amount = Convert.ToDouble(instrec.Amount_LC);
            foreach (var item in res)
            {
                if(Amount >= item.Amount_LC  )
                {
                    Amount = Amount - Convert.ToDouble(item.Amount_LC);

                    Mapping_Installment_Planned_Recevied mipr = new Mapping_Installment_Planned_Recevied()
                    {
                        InstallmentReceived_Id = instrec.Id,
                        Amount = Amount,
                        InstallmentPlan_Id = item.Id
                    };
                    db.Mapping_Installment_Planned_Recevied.Add(mipr);
                    db.SaveChanges();

                    var res1 = item;
                    res1.Status = 1;
                    db.Installment_Plan.Attach(res1);
                    db.Entry(res).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }

            Balance_Installments bal = new Balance_Installments()
            {
                Received_Amount = instrec.Amount_LC,
                Plot_id = instrec.Plot_Id,
                InstRece_Id = instrec.Id,
                Transaction_Party = instrec.Customer_Id,
                Due_Amount = 0
            };
            db.Balance_Installments.Add(bal);
            db.SaveChanges();


            

            return Json(true);
        }



    }
}