﻿using DSTS.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DSTS.Controllers
{
	public class LoginController : Controller
	{
		business bl = new business();
		// GET: Login
		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(string kulAdi, string parola)
		{
			var data = bl.Login(kulAdi, parola);
			if (data.personelAdi != null)
			{
				Session.Timeout = 120;
				Session.Add("kullaniciAdi", data.kullaniciAdi);
				Session.Add("yetki", data.yekiId);
				return Redirect("/Arama/OdaAdinaGoreAra");
			}
			else
			{
				ViewBag.mesaj = "Kullanıcı Adı yada parola hatalı!";
				return View();
			}
		}
	}
}