using BLL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UI.Areas.Admin.Controllers
{
    public class AddressController : Controller
    {
        // GET: Admin/Address
        private AddressBLL bll = new AddressBLL();

        public ActionResult AddressList()
        {
            List<AddressDTO> list = new List<AddressDTO>();
            list = bll.GetAddress();
            return View(list);
        }

        public ActionResult AddAddress()
        {
            AddressDTO dto = new AddressDTO();
            return View(dto);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddAddress(AddressDTO model)
        {
            if (ModelState.IsValid)
            {
                if (bll.Address(model))
                {
                    ViewBag.ProcessState = General.Messages.AddSuccess;
                    ModelState.Clear();
                    model = new AddressDTO();
                }
                else
                    ViewBag.ProcessState = General.Messages.GeneralError;
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            return View(model);
        }

        public ActionResult UpdateAddress(int ID)
        {
            List<AddressDTO> list = new List<AddressDTO>();
            list = bll.GetAddress();
            AddressDTO dto = list.First(x => x.ID == ID);
            return View(dto);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateAddress(AddressDTO model)
        {
            if (ModelState.IsValid)
            {
                if (bll.UpdateAddress(model))
                    ViewBag.ProcessState = General.Messages.UpdateSuccess;
                else
                    ViewBag.ProcessState = General.Messages.GeneralError;
            }
            else
            {
                ViewBag.ProcessState = General.Messages.EmptyArea;
            }
            return View(model);
        }

        public JsonResult DeleteAddress(int ID)
        {
            bll.DeleteAddress(ID);
            return Json("");
        }
    }
}