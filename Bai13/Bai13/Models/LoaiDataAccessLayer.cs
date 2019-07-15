using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Bai13.Models
{
    public class LoaiDataAccessLayer
    {

        public IEnumerable<Loai> GetLoais()
        {
            List<Loai> dsLoai = new List<Loai>();
            using (var dtLoai = DataProvider.SelectData("spLayTatCaLoai",
                CommandType.StoredProcedure, null))
            {
                foreach (DataRow row in dtLoai.Rows)
                {
                    Loai lo = new Loai();
                    lo.MaLoai = Convert.ToInt32(row["MaLoai"]);
                    lo.TenLoai = row["TenLoai"].ToString();
                    lo.MoTa = row["MoTa"].ToString();
                    lo.Hinh = row["Hinh"].ToString();
                    //var l=row.Get<Loai>(); 
                    dsLoai.Add(lo);
                }

                //var list = dtLoai.ToList<Loai>();

            }


            return dsLoai;
        }

        public int AddLoai(Loai lo)
        {
            SqlParameter[] pa = new SqlParameter[4];
            pa[0] = new SqlParameter("MaLoai", SqlDbType.Int);
            pa[0].Direction = ParameterDirection.Output;

            pa[1] = new SqlParameter("TenLoai", lo.TenLoai);
            pa[2] = new SqlParameter("MoTa", lo.MoTa);
            pa[3] = new SqlParameter("Hinh", lo.Hinh);
            DataProvider.ExcuteNonQuery("spThemLoai", CommandType.StoredProcedure, pa);
            return (int) pa[0].Value;
        }
    

    public void UpdateLoai(Loai lo)
        {
            SqlParameter[] pa = new SqlParameter[4];
            pa[0] = new SqlParameter("MaLoai", lo.MaLoai);
            pa[1] = new SqlParameter("TenLoai", lo.TenLoai);
            pa[2] = new SqlParameter("MoTa", lo.MoTa);
            pa[3] = new SqlParameter("Hinh", lo.Hinh);
            DataProvider.ExcuteNonQuery("spSuaLoai", CommandType.StoredProcedure, pa);
        }

        public Loai GetLoai(int? id)
        {
            if (!id.HasValue) return null;
            SqlParameter[] pa = new SqlParameter[1];
            pa[0] = new SqlParameter("MaLoai", id);
            DataTable dtLoai = DataProvider.SelectData("spLayLoai",
                CommandType.StoredProcedure, pa);
            if (dtLoai.Rows.Count > 0)
            {
                var row = dtLoai.Rows[0];
                return new Loai()
                {
                    MaLoai = Convert.ToInt32(row["MaLoai"]),
                    TenLoai = row["TenLoai"].ToString(),
                    MoTa = row["MoTa"].ToString(),
                    Hinh = row["Hinh"].ToString()
                };
            }
            return null;
        }
        //To Delete the record on a particular employee
        public void DeleteLoai(int? id)
        {
            if (!id.HasValue) return;
            SqlParameter[] pa = new SqlParameter[1];
            pa[0] = new SqlParameter("MaLoai", id);
            DataTable dtLoai = DataProvider.SelectData("spXoaLoai",
                CommandType.StoredProcedure, pa);
        }
    }

    //public static class NgoaiLe
    //{
        
    //    public static T UpdateIntoDatabase<T>(this T row) where T : class, new()
    //    {
    //        T obj = new T();
    //        var paras=new SqlParameter[obj.GetType().GetProperties().Length];
    //        var props = obj.GetType().GetProperties();
    //        for (var index = 0; index < props.Length; index++)
    //        {
    //            var prop = props[index];
    //            paras[index].Value = prop.GetValue(null);
    //        }

    //        DataProvider.ExcuteNonQuery("spSua"+typeof(T).Name, CommandType.StoredProcedure, paras);

    //        return obj;
    //    }

    //    public static T Get<T>(this DataRow row) where T : class, new()
    //    {
    //        T obj = new T();
    //        foreach (var prop in obj.GetType().GetProperties())
    //        {
    //            try
    //            {
    //                PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
    //                propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
    //            }
    //            catch
    //            {
    //                continue;
    //            }
    //        }

    //        return obj;
    //    }

    //    public static T ConvertToLoai<T>(this DataRow row) where T : class, new()
    //    {
    //        dynamic obj = new T();
    //        if (HasProperty(obj, "MaLoai"))
    //        {
    //            obj.MaLoai = Convert.ToInt32(row["MaLoai"]);
    //        }
    //        if (HasProperty(obj, "TenLoai"))
    //        {
    //            obj.TenLoai = Convert.ToInt32(row["TenLoai"]);
    //        }
    //        if (HasProperty(obj, "MoTa"))
    //        {
    //            obj.MoTa = Convert.ToInt32(row["MoTa"]);
    //        }
    //        if (HasProperty(obj, "Hinh"))
    //        {
    //            obj.Hinh = Convert.ToInt32(row["Hinh"]);
    //        }
    //        return obj;
    //    }

    //    public static bool HasProperty(dynamic obj, string name)
    //    {
    //        Type objType = obj.GetType();

    //        if (objType == typeof(ExpandoObject))
    //        {
    //            return ((IDictionary<string, object>)obj).ContainsKey(name);
    //        }

    //        return objType.GetProperty(name) != null;
    //    }
    //    public static List<T> ToList<T>(this DataTable table) where T : class, new()
    //    {
    //        try
    //        {
    //            List<T> list = new List<T>();

    //            foreach (DataRow row in table.Rows)
    //            {
    //                T obj = new T();

    //                foreach (var prop in obj.GetType().GetProperties())
    //                {
    //                    try
    //                    {
    //                        PropertyInfo propertyInfo = obj.GetType().GetProperty(prop.Name);
    //                        propertyInfo.SetValue(obj, Convert.ChangeType(row[prop.Name], propertyInfo.PropertyType), null);
    //                    }
    //                    catch
    //                    {
    //                        continue;
    //                    }
    //                }

    //                list.Add(obj);
    //            }

    //            return list;
    //        }
    //        catch
    //        {
    //            return null;
    //        }
    //    }
    //}
}