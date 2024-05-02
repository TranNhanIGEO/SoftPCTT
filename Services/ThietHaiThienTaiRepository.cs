using Dapper;
using System.Data;
using WebApi.Models;
namespace WebApi.Services;

public class ThietHaiThienTaiRepository : BaseRepository{
    public ThietHaiThienTaiRepository(IDbConnection connection) : base(connection){}
    public IEnumerable<ThietHaiThienTai> GetThietHaiThienTais(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        // trường hợp tìm kiếm theo từng quận huyện (truyền mã huyện)
        if (mahuyen != "null"){
            //trường hợp tìm kiếm theo từng quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<ThietHaiThienTai>("SELECT a.objectid, a.idthiethai, a.loaithientai, a.doituongthiethai, a.motathiethai, a.dvtthiethai, a.soluong, a.giatrithiethai, a.diadiem, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, a.year, a.maxa, a.mahuyen, a.namcapnhat, a.ghichu FROM thiethai_thientai a WHERE (" + SqlQuery + ") AND a.MaHuyen = @_mahuyen ORDER BY a.ngay ASC", new{
                    _mahuyen = mahuyen
                });
            }
            // trường hợp tìm kiếm theo từng quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<ThietHaiThienTai>("SELECT * FROM GetThietHaiThienTais(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
        // trường hợp tìm kiếm tất cả quận huyện (không truyền mã huyện)
        else{
            // trường hợp tìm kiếm tất cả quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<ThietHaiThienTai>("SELECT a.objectid, a.idthiethai, a.loaithientai, a.doituongthiethai, a.motathiethai, a.dvtthiethai, a.soluong, a.giatrithiethai, a.diadiem, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, a.year, a.maxa, a.mahuyen, a.namcapnhat, a.ghichu FROM thiethai_thientai a WHERE " + SqlQuery + " ORDER BY a.ngay ASC");
            }
            // trường hợp tìm kiếm tất cả quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<ThietHaiThienTai>("SELECT * FROM GetThietHaiThienTais(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
    }
    public IEnumerable<ThietHaiThienTaiDetailStatistics> GetDammageDetailStatistics(string Mahuyen, string? SqlQuery){  
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }     
        if (Mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<ThietHaiThienTaiDetailStatistics>("SELECT a.diadiem AS tenhuyen, a.loaithientai, a.doituongthiethai,a.motathiethai, SUM(a.soluong) AS Soluong, a.dvtthiethai, c.color AS mamau FROM thiethai_thientai a LEFT JOIN Color c ON a.doituongthiethai = c.name WHERE a.mahuyen = @_mahuyen AND " + SqlQuery + " GROUP BY a.diadiem, a.loaithientai, a.doituongthiethai, a.motathiethai, a.dvtthiethai, c.color ORDER BY a.diadiem ASC"
                , new{
                    _mahuyen = Mahuyen
                });
            }
            return connection.Query<ThietHaiThienTaiDetailStatistics>("SELECT a.diadiem AS tenhuyen, a.loaithientai, a.doituongthiethai,a.motathiethai, SUM(a.soluong) AS Soluong, a.dvtthiethai, c.color AS mamau FROM thiethai_thientai a LEFT JOIN Color c ON a.doituongthiethai = c.name WHERE a.mahuyen = @_mahuyen GROUP BY a.diadiem, a.loaithientai, a.doituongthiethai, a.motathiethai, a.dvtthiethai, c.color ORDER BY a.diadiem ASC"
                , new{
                    _mahuyen = Mahuyen
                });
        }else{
            if (SqlQuery != "null"){
                return connection.Query<ThietHaiThienTaiDetailStatistics>("SELECT a.diadiem AS tenhuyen, a.loaithientai, a.doituongthiethai,a.motathiethai, SUM(a.soluong) AS Soluong, a.dvtthiethai, c.color AS mamau FROM thiethai_thientai a LEFT JOIN Color c ON a.doituongthiethai = c.name WHERE " + SqlQuery + " GROUP BY a.diadiem, a.loaithientai, a.doituongthiethai, a.motathiethai, a.dvtthiethai, c.color ORDER BY a.diadiem ASC");
            }
            return connection.Query<ThietHaiThienTaiDetailStatistics>("SELECT a.diadiem AS tenhuyen, a.loaithientai, a.doituongthiethai,a.motathiethai, SUM(a.soluong) AS Soluong, a.dvtthiethai, c.color AS mamau FROM thiethai_thientai a LEFT JOIN Color c ON a.doituongthiethai = c.name GROUP BY a.diadiem, a.loaithientai, a.doituongthiethai, a.motathiethai, a.dvtthiethai, c.color ORDER BY a.diadiem ASC");
        }
    }
    public IEnumerable<ThietHaiThienTaiTotalStatistics> GetDammageTotalStatistics(string Mahuyen, string? SqlQuery){    
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }   
        if (Mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<ThietHaiThienTaiTotalStatistics>("SELECT  a.doituongthiethai, SUM(a.soluong) AS soluong, c.color AS mamau, 'Đối tượng thiệt hại' AS phamvithongke FROM thiethai_thientai a LEFT JOIN Color c ON a.doituongthiethai = c.name WHERE a.mahuyen = @_mahuyen AND " + SqlQuery + " GROUP BY a.doituongthiethai, c.color ORDER BY a.doituongthiethai ASC"
                , new{
                    _mahuyen = Mahuyen
                });
            }
            return connection.Query<ThietHaiThienTaiTotalStatistics>("SELECT  a.doituongthiethai, SUM(a.soluong) AS soluong, c.color AS mamau, 'Đối tượng thiệt hại' AS phamvithongke FROM thiethai_thientai a LEFT JOIN Color c ON a.doituongthiethai = c.name WHERE a.mahuyen = @_mahuyen GROUP BY a.doituongthiethai, c.color ORDER BY a.doituongthiethai ASC"
                , new{
                    _mahuyen = Mahuyen
                });
        }else{
            if (SqlQuery != "null"){
                return connection.Query<ThietHaiThienTaiTotalStatistics>("SELECT  a.doituongthiethai, SUM(a.soluong) AS soluong, c.color AS mamau, 'Đối tượng thiệt hại' AS phamvithongke FROM thiethai_thientai a LEFT JOIN Color c ON a.doituongthiethai = c.name WHERE " + SqlQuery + " GROUP BY a.doituongthiethai, c.color ORDER BY a.doituongthiethai ASC");
            }
            return connection.Query<ThietHaiThienTaiTotalStatistics>("SELECT  a.doituongthiethai, SUM(a.soluong) AS soluong, c.color AS mamau, 'Đối tượng thiệt hại' AS phamvithongke FROM thiethai_thientai a LEFT JOIN Color c ON a.doituongthiethai = c.name GROUP BY a.doituongthiethai, c.color ORDER BY a.doituongthiethai ASC");
        }
    }
    public ThietHaiThienTai? GetThietHaiThienTai(int objectid){
        return connection.QueryFirstOrDefault<ThietHaiThienTai>("SELECT * FROM GetThietHaiThienTai(@_objectid)", new{
            _objectid = objectid
        });
    }
    public int GetMaxObjectId(){
        return connection.QueryFirstOrDefault<int>("SELECT MAX(ObjectId) FROM ThietHai_ThienTai", commandType: CommandType.Text);
    } 
    public int Add(ThietHaiThienTai obj){ 
        DateTime ngay = Convert.ToDateTime(obj.ngay);
        string? nullstring = null!;
        double? soluong = obj.soluong == null ? null : Convert.ToDouble(obj.soluong);
        double? giatrithiethai = obj.giatrithiethai == null ? null : Convert.ToDouble(obj.giatrithiethai);
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);

        if (obj.ngay != null){
            string[] date = obj.ngay.ToString().Split('-');
            return connection.ExecuteScalar<int>("AddThietHaiThienTai", 
                new{
                    _objectid = obj.objectid,
                    _idthiethai = "THTT" +  obj.objectid.ToString(),
                    _loaithientai = obj.loaithientai,
                    _doituongthiethai =obj.doituongthiethai ,
                    _motathiethai = obj.motathiethai,
                    _dvtthiethai = obj.dvtthiethai,
                    _soluong = soluong,
                    _giatrithiethai = giatrithiethai,
                    _diadiem = obj.diadiem,
                    _gio = obj.gio,
                    _ngay = ngay,
                    _maxa = obj.maxa,
                    _mahuyen = obj.mahuyen,
                    _namcapnhat = namcapnhat,
                    _ghichu = nullstring,
                    _day = Convert.ToInt32(date[2]),
                    _month = Convert.ToInt32(date[1]),
                    _year = Convert.ToInt32(date[0]),
                    _date = nullstring
                }, commandType: CommandType.StoredProcedure
            );
        }
        DateTime? ngaynull = null;
        int? nullDMY = null;        
        return connection.ExecuteScalar<int>("AddThietHaiThienTai", 
            new{
                    _objectid = obj.objectid,
                    _idthiethai = "THTT" +  obj.objectid.ToString(),
                    _loaithientai = obj.loaithientai,
                    _doituongthiethai =obj.doituongthiethai ,
                    _motathiethai = obj.motathiethai,
                    _dvtthiethai = obj.dvtthiethai,
                    _soluong = soluong,
                    _giatrithiethai = giatrithiethai,
                    _diadiem = obj.diadiem,
                    _gio = obj.gio,
                    _ngay = ngaynull,
                    _maxa = obj.maxa,
                    _mahuyen = obj.mahuyen,
                    _namcapnhat = namcapnhat,
                    _ghichu = nullstring,
                    _day = nullDMY,
                    _month = nullDMY,
                    _year = nullDMY,
                    _date = nullstring
            }, commandType: CommandType.StoredProcedure
        );
    }
    public int Edit(int objectid, ThietHaiThienTai obj){ 
        DateTime ngay = Convert.ToDateTime(obj.ngay);
        string? nullstring = null!;
        double? soluong = obj.soluong == null ? null : Convert.ToDouble(obj.soluong);
        double? giatrithiethai = obj.giatrithiethai == null ? null : Convert.ToDouble(obj.giatrithiethai);
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);

        if (obj.ngay != null){
            string[] date = obj.ngay.ToString().Split('-');
            return connection.ExecuteScalar<int>("EditThietHaiThienTai", 
                new{
                    _objectid = objectid,
                    _loaithientai = obj.loaithientai,
                    _doituongthiethai =obj.doituongthiethai ,
                    _motathiethai = obj.motathiethai,
                    _dvtthiethai = obj.dvtthiethai,
                    _soluong = soluong,
                    _giatrithiethai = giatrithiethai,
                    _diadiem = obj.diadiem,
                    _gio = obj.gio,
                    _ngay = ngay,
                    _maxa = obj.maxa,
                    _mahuyen = obj.mahuyen,
                    _namcapnhat = namcapnhat,
                    _ghichu = nullstring,
                    _day = Convert.ToInt32(date[2]),
                    _month = Convert.ToInt32(date[1]),
                    _year = Convert.ToInt32(date[0]),
                    _date = nullstring
                }, commandType: CommandType.StoredProcedure
            );
        }
        DateTime? ngaynull = null;
        int? nullDMY = null;        
        return connection.ExecuteScalar<int>("EditThietHaiThienTai", 
            new{
                    _objectid = objectid,
                    _loaithientai = obj.loaithientai,
                    _doituongthiethai =obj.doituongthiethai ,
                    _motathiethai = obj.motathiethai,
                    _dvtthiethai = obj.dvtthiethai,
                    _soluong = soluong,
                    _giatrithiethai = giatrithiethai,
                    _diadiem = obj.diadiem,
                    _gio = obj.gio,
                    _ngay = ngaynull,
                    _maxa = obj.maxa,
                    _mahuyen = obj.mahuyen,
                    _namcapnhat = namcapnhat,
                    _ghichu = nullstring,
                    _day = nullDMY,
                    _month = nullDMY,
                    _year = nullDMY,
                    _date = nullstring
            }, commandType: CommandType.StoredProcedure
        );
    }
    public int Delete(int objectid){
        return connection.ExecuteScalar<int>("DeleteThietHaiThienTai", new{
            _objectid = objectid
        }, commandType: CommandType.StoredProcedure);
    }
}