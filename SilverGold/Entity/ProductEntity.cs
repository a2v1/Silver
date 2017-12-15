using SilverGold.Comman;
using SilverGold.Helper;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SilverGold.Entity
{
    public class ProductEntity
    {
        public string Category { get; set; }
        public string Unit { get; set; }
        public Decimal Weight_Packet { get; set; }
        public string ProductName { get; set; }
        public string SubGroup { get; set; }
        public string PGroup { get; set; }
        public Decimal Opening { get; set; }
        public Decimal Pcs { get; set; }
        public Decimal Tunch { get; set; }
        public Decimal Westage { get; set; }
        public Decimal LabourRate { get; set; }
        public Decimal Fine { get; set; }
        public Decimal Amount { get; set; }
        public string RawDefine { get; set; }
        public DateTime OpenDate { get; set; }
        public string Narration { get; set; }
        public string Company { get; set; }
        public string UserId { get; set; }

        public void AddProductEntity(string _Category, string _Unit, Decimal _Weight_Packet, string _ProductName, string _SubGroup, string _PGroup, Decimal _Opening, Decimal _Pcs, Decimal _Tunch, Decimal _Westage, Decimal _LabourRate, Decimal _Fine, Decimal _Amount, string _RawDefine, DateTime _OpenDate, string _Narration, string _Company, string _UserId)
        {
            Category = _Category;
            Unit = _Unit;
            Weight_Packet = _Weight_Packet;
            ProductName = _ProductName;
            PGroup = _PGroup;
            Opening = _Opening;
            Pcs = _Pcs;
            Tunch = _Tunch;
            Westage = _Westage;
            LabourRate = _LabourRate;
            Fine = _Fine;
            Amount = _Amount;
            RawDefine = _RawDefine;
            OpenDate = _OpenDate;
            Narration = _Narration;
            Company = _Company;
            UserId = _UserId;
        }

    }

    public static class ProductFactory
    {
        public static List<ProductEntity> GetProductDetails()
        {
            List<ProductEntity> ProductList = new List<ProductEntity>();
            try
            {
                using (OleDbConnection con = new OleDbConnection(ConnectionClass.LoginConString(CommanHelper.Com_DB_PATH, CommanHelper.Com_DB_NAME + ".mdb")))
                {
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("Select Category,Unit,Weight_Packet,ProductName,SubGroup,PGroup,Round(Opening,3) AS Opening,Round(Pcs,3) AS Pcs,Round(Tunch,3) AS Tunch,Round(Westage,3) AS Westage,Round(LabourRate,3) AS LabourRate,Round(Fine,3) AS Fine,Round(Amount,3) AS Amount,RawDefine,OpenDate,Narration From Product", con);
                    OleDbDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        ProductEntity oProductEntity = new ProductEntity();
                        oProductEntity.Category = dr["Category"].ToString();
                        oProductEntity.Unit = dr["Unit"].ToString();
                        oProductEntity.Weight_Packet = Conversion.ConToDec5(dr["Weight_Packet"].ToString());
                        oProductEntity.ProductName = dr["ProductName"].ToString();
                        oProductEntity.SubGroup = dr["SubGroup"].ToString();
                        oProductEntity.PGroup = dr["PGroup"].ToString();
                        oProductEntity.Opening = Conversion.ConToDec5(dr["Opening"].ToString());
                        oProductEntity.Pcs = Conversion.ConToDec5(dr["Pcs"].ToString());
                        oProductEntity.Tunch = Conversion.ConToDec5(dr["Tunch"].ToString());
                        oProductEntity.Westage = Conversion.ConToDec5(dr["Westage"].ToString());
                        oProductEntity.LabourRate = Conversion.ConToDec5(dr["LabourRate"].ToString());
                        oProductEntity.Fine = Conversion.ConToDec5(dr["Fine"].ToString());
                        oProductEntity.Amount = Conversion.ConToDec5(dr["Amount"].ToString());
                        oProductEntity.RawDefine = dr["RawDefine"].ToString();
                        oProductEntity.OpenDate = Conversion.ConToDT(dr["OpenDate"].ToString());
                        oProductEntity.Narration = dr["Narration"].ToString();
                        ProductList.Add(oProductEntity);
                    }

                    con.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return ProductList;
        }
    }
}
