using Dapper;
using System.Data;
using WebApi.Models;
namespace WebApi.Services;

public class SatLoLineRepository : BaseRepository{
    public SatLoLineRepository(IDbConnection connection) : base(connection){}
    public IEnumerable<SatLoLine> GetSatLoLines(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        // trường hợp tìm kiếm theo từng quận huyện (truyền mã huyện)
        if (mahuyen != "null"){
            //trường hợp tìm kiếm theo từng quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<SatLoLine>("SELECT a.objectid, a.idsatlol, a.vitri, a.tuyensong, a.capsong, a.chieudai::Numeric, a.chieurong::Numeric, a.mucdo, a.tinhtrang, a.anhhuong, a.khoangcachah, a.ditichah, a.sohoah, a.songuoiah, a.hatangah, a.congtrinhchongsl, a.chudautu, a.tenduan, a.quymoduan, a.tongmucduan, a.tiendothuchien, a.nguongoc, a.dubao, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, a.ctxdke, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape FROM SatLo_Line a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen"
                , new{
                    _mahuyen = mahuyen
                });
            }
            // trường hợp tìm kiếm theo từng quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<SatLoLine>("SELECT * FROM GetSatLoLines(@_mahuyen)"
            , new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
        // trường hợp tìm kiếm tất cả quận huyện (không truyền mã huyện)
        else{
            // trường hợp tìm kiếm tất cả quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<SatLoLine>("SELECT a.objectid, a.idsatlol, a.vitri, a.tuyensong, a.capsong, a.chieudai::Numeric, a.chieurong::Numeric, a.mucdo, a.tinhtrang, a.anhhuong, a.khoangcachah, a.ditichah, a.sohoah, a.songuoiah, a.hatangah, a.congtrinhchongsl, a.chudautu, a.tenduan, a.quymoduan, a.tongmucduan, a.tiendothuchien, a.nguongoc, a.dubao, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, a.ctxdke, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape FROM SatLo_Line a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen WHERE " + SqlQuery + "");
            }
            // trường hợp tìm kiếm tất cả quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<SatLoLine>("SELECT * FROM GetSatLoLines(@_mahuyen)"
            , new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
    }
    public IEnumerable<SatLoLineDetailStatistics> GetSatLoLineDetailStatistics(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        if (mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<SatLoLineDetailStatistics>("SELECT h.tenhuyen AS quan_huyen_tp, a.mucdo AS mucdosatlo, COUNT(a.mucdo) AS Soluongvitrisatlo, SUM(a.chieudai)::Numeric AS Tongchieudaisatlo, c.color AS mamau FROM SatLo_Line a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen LEFT JOIN Color c ON a.mucdo = c.name WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen GROUP BY h.tenhuyen, a.mucdo, c.color ORDER BY h.tenhuyen ASC, CASE WHEN a.mucdo = 'Sạt lở bình thường' THEN 1 WHEN a.mucdo = 'Sạt lở nguy hiểm' THEN 2 ELSE 3 END ASC"
                , new{
                    _mahuyen = mahuyen
                });
            }
                return connection.Query<SatLoLineDetailStatistics>("SELECT h.tenhuyen AS quan_huyen_tp, a.mucdo AS mucdosatlo, COUNT(a.mucdo) AS Soluongvitrisatlo, SUM(a.chieudai)::Numeric AS Tongchieudaisatlo, c.color AS mamau FROM SatLo_Line a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen LEFT JOIN Color c ON a.mucdo = c.name WHERE a.MaHuyen = @_mahuyen GROUP BY h.tenhuyen, a.mucdo, c.color ORDER BY h.tenhuyen ASC, CASE WHEN a.mucdo = 'Sạt lở bình thường' THEN 1 WHEN a.mucdo = 'Sạt lở nguy hiểm' THEN 2 ELSE 3 END ASC"
                , new{
                    _mahuyen = mahuyen
            });      
        }
        else{
            if (SqlQuery != "null"){
                return connection.Query<SatLoLineDetailStatistics>("SELECT h.tenhuyen AS quan_huyen_tp, a.mucdo AS mucdosatlo, COUNT(a.mucdo) AS Soluongvitrisatlo, SUM(a.chieudai)::Numeric AS Tongchieudaisatlo, c.color AS mamau FROM SatLo_Line a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen LEFT JOIN Color c ON a.mucdo = c.name WHERE " + SqlQuery + " GROUP BY h.tenhuyen, a.mucdo, c.color ORDER BY h.tenhuyen ASC, CASE WHEN a.mucdo = 'Sạt lở bình thường' THEN 1 WHEN a.mucdo = 'Sạt lở nguy hiểm' THEN 2 ELSE 3 END ASC");            
            }
            return connection.Query<SatLoLineDetailStatistics>("SELECT h.tenhuyen AS quan_huyen_tp, a.mucdo AS mucdosatlo, COUNT(a.mucdo) AS Soluongvitrisatlo, SUM(a.chieudai)::Numeric AS Tongchieudaisatlo, c.color AS mamau FROM SatLo_Line a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen LEFT JOIN Color c ON a.mucdo = c.name GROUP BY h.tenhuyen, a.mucdo, c.color ORDER BY h.tenhuyen ASC, CASE WHEN a.mucdo = 'Sạt lở bình thường' THEN 1 WHEN a.mucdo = 'Sạt lở nguy hiểm' THEN 2 ELSE 3 END ASC");  
        }       
    } 
    public IEnumerable<SatLoLineTotalStatistics> GetSatLoLineTotalStatistics(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        if (mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<SatLoLineTotalStatistics>("(SELECT  a.mucdo AS mucdosatlo, COUNT(a.mucdo) AS Soluongvitrisatlo, SUM(a.chieudai)::Numeric AS Tongchieudaisatlo, c.color AS mamau, 'Mức độ sạt lở' AS phamvithongke FROM SatLo_Line a  LEFT JOIN Color c ON a.mucdo = c.name WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen GROUP BY  a.mucdo, c.color ORDER BY CASE WHEN a.mucdo = 'Sạt lở bình thường' THEN 1 WHEN a.mucdo = 'Sạt lở nguy hiểm' THEN 2 ELSE 3 END ASC)"
                , new{
                    _mahuyen = mahuyen
                });
            }
            return connection.Query<SatLoLineTotalStatistics>("(SELECT  a.mucdo AS mucdosatlo, COUNT(a.mucdo) AS Soluongvitrisatlo, SUM(a.chieudai)::Numeric AS Tongchieudaisatlo, c.color AS mamau, 'Mức độ sạt lở' AS phamvithongke FROM SatLo_Line a  LEFT JOIN Color c ON a.mucdo = c.name WHERE a.MaHuyen = @_mahuyen GROUP BY  a.mucdo, c.color ORDER BY CASE WHEN a.mucdo = 'Sạt lở bình thường' THEN 1 WHEN a.mucdo = 'Sạt lở nguy hiểm' THEN 2 ELSE 3 END ASC)"
                , new{
                    _mahuyen = mahuyen
                });
        }else{
            if (SqlQuery != "null"){
                return connection.Query<SatLoLineTotalStatistics>("(SELECT  a.mucdo AS mucdosatlo, COUNT(a.mucdo) AS Soluongvitrisatlo, SUM(a.chieudai)::Numeric AS Tongchieudaisatlo, c.color AS mamau, 'Mức độ sạt lở' AS phamvithongke FROM SatLo_Line a  LEFT JOIN Color c ON a.mucdo = c.name WHERE " + SqlQuery + " GROUP BY  a.mucdo, c.color ORDER BY CASE WHEN a.mucdo = 'Sạt lở bình thường' THEN 1 WHEN a.mucdo = 'Sạt lở nguy hiểm' THEN 2 ELSE 3 END ASC)");
            }
            return connection.Query<SatLoLineTotalStatistics>("(SELECT  a.mucdo AS mucdosatlo, COUNT(a.mucdo) AS Soluongvitrisatlo, SUM(a.chieudai)::Numeric AS Tongchieudaisatlo, c.color AS mamau, 'Mức độ sạt lở' AS phamvithongke FROM SatLo_Line a LEFT JOIN Color c ON a.mucdo = c.name GROUP BY  a.mucdo, c.color ORDER BY CASE WHEN a.mucdo = 'Sạt lở bình thường' THEN 1 WHEN a.mucdo = 'Sạt lở nguy hiểm' THEN 2 ELSE 3 END ASC)");
        }
    } 
    public SatLoLine? GetSatLoLine(int objectid){
        return connection.QueryFirstOrDefault<SatLoLine>("SELECT * FROM GetSatLoLine(@_objectid)", new{
            _objectid = objectid
        }, commandType: CommandType.Text);
    }
    public int GetMaxObjectId(){
        return connection.QueryFirstOrDefault<int>("SELECT MAX(ObjectId) FROM SatLo_Line", commandType: CommandType.Text);
    }
    public int Add(SatLoLine obj, int objectid){ 
        double? nulltoado = null;
        string? nullstring = null!;
        double? chieudai = obj.chieudai == null ? null : Convert.ToDouble(obj.chieudai);
        double? chieurong = obj.chieurong == null ? null : Convert.ToDouble(obj.chieurong);
        double? ditichah = obj.ditichah == null ? null : Convert.ToDouble(obj.ditichah);
        int? sohoah = obj.sohoah == null ? null : Convert.ToInt32(obj.sohoah);
        int? songuoiah = obj.songuoiah == null ? null : Convert.ToInt32(obj.songuoiah);
        int? tongmucduan = obj.tongmucduan == null ? null : Convert.ToInt32(obj.tongmucduan);
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);

        if (obj.toado != null){
            string toado = obj.toado.Replace("((", string.Empty).Replace("))", string.Empty);
            return connection.ExecuteScalar<int>("AddSatLoLine", 
                new{
                    _objectid = objectid,
                    _idsatlol = "SL" +  objectid.ToString(),
                    _vitri = obj.vitri,
                    _tuyensong = obj.tuyensong,
                    _capsong = obj.capsong,
                    _chieudai = chieudai,
                    _chieurong = chieurong,
                    _mucdo = obj.mucdo,
                    _tinhtrang = obj.tinhtrang,
                    _anhhuong = obj.anhhuong,
                    _khoangcachah = obj.khoangcachah,
                    _ditichah = ditichah,
                    _sohoah = sohoah,
                    _songuoiah = songuoiah,
                    _hatangah = obj.hatangah,
                    _congtrinhchongsl = obj.congtrinhchongsl,
                    _chudautu = obj.chudautu,
                    _tenduan = obj.tenduan,
                    _quymoduan = obj.quymoduan,
                    _tongmucduan = tongmucduan,
                    _tiendothuchien = obj.tiendothuchien,
                    _nguongoc = obj.nguongoc,
                    _dubao = obj.dubao,
                    _maxa = obj.maxa,
                    _mahuyen = obj.mahuyen,
                    _namcapnhat = namcapnhat,
                    _ghichu = nullstring,
                    _ctxdke = obj.ctxdke,
                    _shape_length = nulltoado,
                    _toado = toado
                }, commandType: CommandType.StoredProcedure
            );
        }
        return connection.ExecuteScalar<int>("AddSatLoLine", 
            new{
                    _objectid = objectid,
                    _idsatlol = "SL" +  objectid.ToString(),
                    _vitri = obj.vitri,
                    _tuyensong = obj.tuyensong,
                    _capsong = obj.capsong,
                    _chieudai = chieudai,
                    _chieurong = chieurong,
                    _mucdo = obj.mucdo,
                    _tinhtrang = obj.tinhtrang,
                    _anhhuong = obj.anhhuong,
                    _khoangcachah = obj.khoangcachah,
                    _ditichah = ditichah,
                    _sohoah = sohoah,
                    _songuoiah = songuoiah,
                    _hatangah = obj.hatangah,
                    _congtrinhchongsl = obj.congtrinhchongsl,
                    _chudautu = obj.chudautu,
                    _tenduan = obj.tenduan,
                    _quymoduan = obj.quymoduan,
                    _tongmucduan = tongmucduan,
                    _tiendothuchien = obj.tiendothuchien,
                    _nguongoc = obj.nguongoc,
                    _dubao = obj.dubao,
                    _maxa = obj.maxa,
                    _mahuyen = obj.mahuyen,
                    _namcapnhat = namcapnhat,
                    _ghichu = nullstring,
                    _ctxdke = obj.ctxdke,
                    _shape_length = nulltoado,
                    _toado = nullstring
            }, commandType: CommandType.StoredProcedure
        );
    }
    public int Edit(int objectid, SatLoLine obj){ 
        double? nulltoado = null;
        string? nullstring = null!;
        double? chieudai = obj.chieudai == null ? null : Convert.ToDouble(obj.chieudai);
        double? chieurong = obj.chieurong == null ? null : Convert.ToDouble(obj.chieurong);
        double? ditichah = obj.ditichah == null ? null : Convert.ToDouble(obj.ditichah);
        int? sohoah = obj.sohoah == null ? null : Convert.ToInt32(obj.sohoah);
        int? songuoiah = obj.songuoiah == null ? null : Convert.ToInt32(obj.songuoiah);
        int? tongmucduan = obj.tongmucduan == null ? null : Convert.ToInt32(obj.tongmucduan);
        short? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt16(obj.namcapnhat);

        if (obj.toado != null){
            string toado = obj.toado.Replace("((", string.Empty).Replace("))", string.Empty);
            return connection.ExecuteScalar<int>("EditSatLoLine", 
                new{
                    _objectid = objectid,
                    _vitri = obj.vitri,
                    _tuyensong = obj.tuyensong,
                    _capsong = obj.capsong,
                    _chieudai = chieudai,
                    _chieurong = chieurong,
                    _mucdo = obj.mucdo,
                    _tinhtrang = obj.tinhtrang,
                    _anhhuong = obj.anhhuong,
                    _khoangcachah = obj.khoangcachah,
                    _ditichah = ditichah,
                    _sohoah = sohoah,
                    _songuoiah = songuoiah,
                    _hatangah = obj.hatangah,
                    _congtrinhchongsl = obj.congtrinhchongsl,
                    _chudautu = obj.chudautu,
                    _tenduan = obj.tenduan,
                    _quymoduan = obj.quymoduan,
                    _tongmucduan = tongmucduan,
                    _tiendothuchien = obj.tiendothuchien,
                    _nguongoc = obj.nguongoc,
                    _dubao = obj.dubao,
                    _maxa = obj.maxa,
                    _mahuyen = obj.mahuyen,
                    _namcapnhat = namcapnhat,
                    _ghichu = nullstring,
                    _ctxdke = obj.ctxdke,
                    _shape_length = nulltoado,
                    _toado = toado
                }, commandType: CommandType.StoredProcedure
            );
        }
        return connection.ExecuteScalar<int>("EditSatLoLine", 
            new{
                    _objectid = objectid,
                    _vitri = obj.vitri,
                    _tuyensong = obj.tuyensong,
                    _capsong = obj.capsong,
                    _chieudai = chieudai,
                    _chieurong = chieurong,
                    _mucdo = obj.mucdo,
                    _tinhtrang = obj.tinhtrang,
                    _anhhuong = obj.anhhuong,
                    _khoangcachah = obj.khoangcachah,
                    _ditichah = ditichah,
                    _sohoah = sohoah,
                    _songuoiah = songuoiah,
                    _hatangah = obj.hatangah,
                    _congtrinhchongsl = obj.congtrinhchongsl,
                    _chudautu = obj.chudautu,
                    _tenduan = obj.tenduan,
                    _quymoduan = obj.quymoduan,
                    _tongmucduan = tongmucduan,
                    _tiendothuchien = obj.tiendothuchien,
                    _nguongoc = obj.nguongoc,
                    _dubao = obj.dubao,
                    _maxa = obj.maxa,
                    _mahuyen = obj.mahuyen,
                    _namcapnhat = namcapnhat,
                    _ghichu = nullstring,
                    _ctxdke = obj.ctxdke,
                    _shape_length = nulltoado,
                    _toado = nullstring
            }, commandType: CommandType.StoredProcedure
        );
    }
    public int Delete(int objectid){
        return connection.ExecuteScalar<int>("DeleteSatLoLine", new{
            _objectid = objectid
        });
    }
}