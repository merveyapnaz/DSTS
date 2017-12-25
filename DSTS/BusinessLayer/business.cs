﻿using DSTS.localClass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace DSTS.BusinessLayer
{
    public class business
    {
        string conStrig = WebConfigurationManager.ConnectionStrings["Local_DatabaseConnection"].ConnectionString;

        public void StokEkle(string fakulteAdi, string dbAdi, int DbAdet, decimal dbFiyat, string dbtur, string dbMarka, string dbModel)
        {           
            using (SqlConnection conn = new SqlConnection(conStrig))
            {
                conn.Open();
                
                SqlCommand cmdd = new SqlCommand("spkayit", conn);
                cmdd.CommandType = CommandType.StoredProcedure;
               
                cmdd.Parameters.Add(new SqlParameter("@demirbasFakulte", fakulteAdi));
                cmdd.Parameters.Add(new SqlParameter("@demirbasAdi", dbAdi));                              
                cmdd.Parameters.Add(new SqlParameter("@demirbasAdet", DbAdet));
                cmdd.Parameters.Add(new SqlParameter("@demirbasFiyat", dbFiyat));
                cmdd.Parameters.Add(new SqlParameter("@demirbasTur", dbtur));
                cmdd.Parameters.Add(new SqlParameter("@demirbasTarih", DateTime.Now));
                cmdd.Parameters.Add(new SqlParameter("@demirbasMarka", dbMarka));
                cmdd.Parameters.Add(new SqlParameter("@demirbasModel", dbModel));

                SqlDataAdapter da = new SqlDataAdapter(cmdd);

                cmdd.CommandTimeout = 600;
                cmdd.ExecuteNonQuery();
                conn.Dispose();               
                conn.Close();
            }
         
        }

		public List<localDemirbas> PersoneleGoreAra(string personelAdi)
		{
			List<localDemirbas> ld = new List<localDemirbas>();
			using (SqlConnection conn = new SqlConnection(conStrig))
			{
				conn.Open();
				SqlCommand cmdd = new SqlCommand("spPersoneleGoreAra", conn);
				cmdd.Parameters.Add(new SqlParameter("@personelAdi", personelAdi));

				cmdd.CommandTimeout = 600;
				cmdd.CommandType = CommandType.StoredProcedure;
				SqlDataReader rd = cmdd.ExecuteReader();
				if (rd.HasRows == true)
				{
					while (rd.Read())
					{
						localDemirbas d = new localDemirbas();
						d.odaAdi = rd["odaAdi"].ToString();
						d.demirbasKod = rd["demirbasKodu"].ToString();
						d.demirbasAdi = rd["demirbasAdi"].ToString();
						d.demirbasMarka = rd["marka"].ToString();
						d.demirbasModel = rd["model"].ToString();
						d.adet = (int)rd["adet"];
						ld.Add(d);
					}
				}
				conn.Dispose();
				conn.Close();
			}
			return ld;
		}

        public List<localFakulte> FakulteAdi()
        {
            List<localFakulte> ld = new List<localFakulte>();
            using (SqlConnection conn = new SqlConnection(conStrig))
            {
                conn.Open();
                SqlCommand cmdd = new SqlCommand("spFakulteGetir", conn);
                

                cmdd.CommandTimeout = 600;
                cmdd.CommandType = CommandType.StoredProcedure;
                SqlDataReader rd = cmdd.ExecuteReader();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        localFakulte d = new localFakulte();
                        d.fakulteAdi = rd["fakulteAdi"].ToString();
                        d.fakulteId = Convert.ToInt32(rd["fakulteId"]);
                        
                        ld.Add(d);
                    }
                }
                conn.Dispose();
                conn.Close();
            }
            return ld;
        }
        public List<localTur> turAdi()
        {
            List<localTur> ld = new List<localTur>();
            using (SqlConnection conn = new SqlConnection(conStrig))
            {
                conn.Open();
                SqlCommand cmdd = new SqlCommand("spTurAdiGetir", conn);


                cmdd.CommandTimeout = 600;
                cmdd.CommandType = CommandType.StoredProcedure;
                SqlDataReader rd = cmdd.ExecuteReader();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        localTur d = new localTur();
                        d.turAdi = rd["turAdi"].ToString();

                        ld.Add(d);
                    }
                }
                conn.Dispose();
                conn.Close();
            }
            return ld;
        }

		public localpersonel spOdayaGorePersonel(string odaAdi)
		{
			localpersonel lp = new localpersonel();

			using (SqlConnection conn = new SqlConnection(conStrig))
			{
				conn.Open();
				SqlCommand cmdd = new SqlCommand("spOdayaGorePersonel", conn);
				cmdd.Parameters.Add(new SqlParameter("@odaAdi", odaAdi));

				cmdd.CommandTimeout = 600;
				cmdd.CommandType = CommandType.StoredProcedure;
				SqlDataReader rd = cmdd.ExecuteReader();
				if (rd.HasRows == true)
				{
					rd.Read();
					lp.personelAdi = rd["personelAdi"].ToString();
					
				}
				conn.Dispose();
				conn.Close();
			}
			return lp;
		}

		public List<localDemirbas> spOdayaGoreDemirbas(string odaAdi)
		{
			List<localDemirbas> ld = new List<localDemirbas>();
			using (SqlConnection conn = new SqlConnection(conStrig))
			{
				conn.Open();
				SqlCommand cmdd = new SqlCommand("spOdayaGoreDemirbas", conn);
				cmdd.Parameters.Add(new SqlParameter("@odaAdi", odaAdi));

				cmdd.CommandTimeout = 600;
				cmdd.CommandType = CommandType.StoredProcedure;
				SqlDataReader rd = cmdd.ExecuteReader();
				if (rd.HasRows == true)
				{
					while(rd.Read())
					{
						localDemirbas d = new localDemirbas();
						d.demirbasKod= rd["demirbasKodu"].ToString();
						d.demirbasAdi = rd["demirbasAdi"].ToString();
						d.tur = rd["turAdi"].ToString();
						d.adet=(int)rd["adet"];
						ld.Add(d);
					}
				}
				conn.Dispose();
				conn.Close();
			}
			return ld;
		}

		public List<localDemirbas> spDemirbasAdinaGoreAra()
		{
			List<localDemirbas> ld = new List<localDemirbas>();
			using (SqlConnection conn = new SqlConnection(conStrig))
			{
				conn.Open();
				SqlCommand cmdd = new SqlCommand("spDemirbasAdinaGoreAra", conn);

				cmdd.CommandTimeout = 600;
				cmdd.CommandType = CommandType.StoredProcedure;
				SqlDataReader rd = cmdd.ExecuteReader();
				if (rd.HasRows == true)
				{
					while (rd.Read())
					{
						localDemirbas d = new localDemirbas();
						d.demirbasId = (int)rd["demirbasId"];
						d.demirbasAdi = rd["demirbasAdi"].ToString();
						ld.Add(d);
					}
				}
				conn.Dispose();
				conn.Close();
			}
			return ld;
		}

		public List<localDemirbas> spTuruneGoreAra()
		{
			List<localDemirbas> ld = new List<localDemirbas>();
			using (SqlConnection conn = new SqlConnection(conStrig))
			{
				conn.Open();
				SqlCommand cmdd = new SqlCommand("spTuruneGoreAra", conn);

				cmdd.CommandTimeout = 600;
				cmdd.CommandType = CommandType.StoredProcedure;
				SqlDataReader rd = cmdd.ExecuteReader();
				if (rd.HasRows == true)
				{
					while (rd.Read())
					{
						localDemirbas d = new localDemirbas();
						d.demirbasId = (int)rd["demirbasId"];
						d.tur = rd["turAdi"].ToString();
						ld.Add(d);
					}
				}
				conn.Dispose();
				conn.Close();
			}
			return ld;
		}

		public List<localDemirbas> spFiyatınaGoreAra()
		{
			List<localDemirbas> ld = new List<localDemirbas>();
			using (SqlConnection conn = new SqlConnection(conStrig))
			{
				conn.Open();
				SqlCommand cmdd = new SqlCommand("spFiyatınaGoreAra", conn);

				cmdd.CommandTimeout = 600;
				cmdd.CommandType = CommandType.StoredProcedure;
				SqlDataReader rd = cmdd.ExecuteReader();
				if (rd.HasRows == true)
				{
					while (rd.Read())
					{
						localDemirbas d = new localDemirbas();
						d.demirbasId = (int)rd["demirbasId"];
						d.demirbasFiyat = (decimal)rd["fiyat"];
						ld.Add(d);
					}
				}
				conn.Dispose();
				conn.Close();
			}
			return ld;
		}

		public List<localDemirbas> spTariheGoreAra()
		{
			List<localDemirbas> ld = new List<localDemirbas>();
			using (SqlConnection conn = new SqlConnection(conStrig))
			{
				conn.Open();
				SqlCommand cmdd = new SqlCommand("spTariheGoreAra", conn);

				cmdd.CommandTimeout = 600;
				cmdd.CommandType = CommandType.StoredProcedure;
				SqlDataReader rd = cmdd.ExecuteReader();
				if (rd.HasRows == true)
				{
					while (rd.Read())
					{
						localDemirbas d = new localDemirbas();
						d.demirbasId = (int)rd["demirbasId"];
						d.demirbasTarih = Convert.ToDateTime(rd["alimTarihi"]);
						ld.Add(d);
					}
				}
				conn.Dispose();
				conn.Close();
			}
			return ld;
		}

	
        public List<localpersonel> PersonelAdi()
        {
            List<localpersonel> lp = new List<localpersonel>();
            using (SqlConnection conn = new SqlConnection(conStrig))
            {
                conn.Open();
                SqlCommand cmdd = new SqlCommand("spPersonelAdiGetir", conn);


                cmdd.CommandTimeout = 600;
                cmdd.CommandType = CommandType.StoredProcedure;
                SqlDataReader rd = cmdd.ExecuteReader();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        localpersonel d = new localpersonel();
                        d.personelAdi = rd["personelAdi"].ToString();
                        d.personelId = Convert.ToInt32(rd["personelId"]);
                        lp.Add(d);
                    }
                }
                conn.Dispose();
                conn.Close();
            }
            return lp;
        }

        public void OdaEkle(string odaAdi, string sorumluAdi, string fakulteAdi)
        {
            using (SqlConnection conn = new SqlConnection(conStrig))
            {
                conn.Open();

                SqlCommand cmdd = new SqlCommand("spOdaKaydet", conn);
                cmdd.CommandType = CommandType.StoredProcedure;

                cmdd.Parameters.Add(new SqlParameter("@odaAdi", odaAdi));
                cmdd.Parameters.Add(new SqlParameter("@sorumluAd", sorumluAdi));
                cmdd.Parameters.Add(new SqlParameter("@fakulteAdi", fakulteAdi));
               

                SqlDataAdapter da = new SqlDataAdapter(cmdd);

                cmdd.CommandTimeout = 600;
                cmdd.ExecuteNonQuery();
                conn.Dispose();
                conn.Close();
            }

        }

        public List<localOda> OdaAdiGetir()
        {
            List<localOda> lo = new List<localOda>();
            using (SqlConnection conn = new SqlConnection(conStrig))
            {
                conn.Open();
                SqlCommand cmdd = new SqlCommand("spOdaGetir", conn);


                cmdd.CommandTimeout = 600;
                cmdd.CommandType = CommandType.StoredProcedure;
                SqlDataReader rd = cmdd.ExecuteReader();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        localOda d = new localOda();
                        d.odaId = Convert.ToInt32(rd["odaId"]);
                        d.fakulteId = Convert.ToInt32(rd["fakulteId"]);
                        d.odaAdi = rd["odaAdi"].ToString();

                        lo.Add(d);
                    }
                }
                conn.Dispose();
                conn.Close();
            }
            return lo;
        }

        public List<localDemirbas> DemirbasAdlariniGetir()
        {
            List<localDemirbas> lda = new List<localDemirbas>();
            using (SqlConnection conn = new SqlConnection(conStrig))
            {
                conn.Open();
                SqlCommand cmdd = new SqlCommand("spDemirbasAdiGetir", conn);


                cmdd.CommandTimeout = 600;
                cmdd.CommandType = CommandType.StoredProcedure;
                SqlDataReader rd = cmdd.ExecuteReader();
                if (rd.HasRows == true)
                {
                    while (rd.Read())
                    {
                        localDemirbas d = new localDemirbas();
                        d.fakulteId = Convert.ToInt32(rd["fakulteId"]);
                        d.demirbasKod = rd["demirbasKodu"].ToString();
                        d.demirbasAdi = rd["demirbasAdi"].ToString();
                        d.demirbasMarka = rd["Marka"].ToString();
                        d.demirbasModel = rd["model"].ToString();

                        lda.Add(d);
                    }
                }
                conn.Dispose();
                conn.Close();
            }
            return lda;
        }

        public void OdayaDemirbasEkle(string odaAdi, string Demirbas, int dbAdet)
        {
            using (SqlConnection conn = new SqlConnection(conStrig))
            {
                conn.Open();

                SqlCommand cmdd = new SqlCommand("spOdayaDemirbasEkle", conn);
                cmdd.CommandType = CommandType.StoredProcedure;

                cmdd.Parameters.Add(new SqlParameter("@oda", odaAdi));
                cmdd.Parameters.Add(new SqlParameter("@demirbas", Demirbas));
                cmdd.Parameters.Add(new SqlParameter("@demirbasAdedi", dbAdet));
                

                SqlDataAdapter da = new SqlDataAdapter(cmdd);

                cmdd.CommandTimeout = 600;
                cmdd.ExecuteNonQuery();
                conn.Dispose();
                conn.Close();
            }

        }

        public void OdaBilgiGuncelle(string odaAdi, string odaId, string personelId)
        {
            using (SqlConnection conn = new SqlConnection(conStrig))
            {
                conn.Open();

                SqlCommand cmdd = new SqlCommand("spOdaBilgiGuncelle", conn);
                cmdd.CommandType = CommandType.StoredProcedure;

                cmdd.Parameters.Add(new SqlParameter("@odaId", Convert.ToInt32(odaId)));
                cmdd.Parameters.Add(new SqlParameter("@odaAdi", odaAdi));
                cmdd.Parameters.Add(new SqlParameter("@personelid", Convert.ToInt32(personelId)));
               
                SqlDataAdapter da = new SqlDataAdapter(cmdd);

                cmdd.CommandTimeout = 600;
                cmdd.ExecuteNonQuery();
                conn.Dispose();
                conn.Close();
            }

        }

    }
        }

		public localpersonel Login(string kulAdi, string parola)
		{
			localpersonel lp = new localpersonel();
			using (SqlConnection conn = new SqlConnection(conStrig))
			{
				conn.Open();

				SqlCommand cmdd = new SqlCommand("spLogin", conn);
				cmdd.Parameters.Add(new SqlParameter("@kulAdi", kulAdi));
				cmdd.Parameters.Add(new SqlParameter("@parola", parola));

				cmdd.CommandTimeout = 600;
				cmdd.CommandType = CommandType.StoredProcedure;
				SqlDataReader rd = cmdd.ExecuteReader();
				if (rd.HasRows == true)
				{
					while (rd.Read())
					{
						lp.personelAdi = rd["personelAdi"].ToString();
						lp.yekiId = (int) rd["yetkiId"];
					}
				}
				conn.Dispose();
				conn.Close();
			}
			return lp;
		}

		public localDemirbas DemirbaslariGetir(int demirbasId)
		{
			localDemirbas ld = new localDemirbas();
			using (SqlConnection conn = new SqlConnection(conStrig))
			{
				conn.Open();

				SqlCommand cmdd = new SqlCommand("spDemirbaslariGetir", conn);
				cmdd.Parameters.Add(new SqlParameter("@demirbasId", demirbasId));

				cmdd.CommandTimeout = 600;
				cmdd.CommandType = CommandType.StoredProcedure;
				SqlDataReader rd = cmdd.ExecuteReader();
				if (rd.HasRows == true)
				{
					while (rd.Read())
					{
						ld.demirbasKod = rd["demirbasKodu"].ToString();
						ld.demirbasAdi = rd["demirbasAdi"].ToString();
						ld.tur = rd["turAdi"].ToString();
						ld.demirbasFiyat = Convert.ToDecimal(rd["fiyat"]);
						ld.demirbasTarih = Convert.ToDateTime(rd["alimTarihi"]);
						ld.demirbasAdet = (int)rd["demirbasAdet"];
						ld.odaAdi = rd["odaAdi"].ToString();
						ld.adet = (int)rd["adet"];
					}
				}
				conn.Dispose();
				conn.Close();
			}
			return ld;
		}
	}

}
