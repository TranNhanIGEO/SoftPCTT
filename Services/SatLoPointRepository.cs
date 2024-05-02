using Dapper;
using System.Data;
using WebApi.Models;
namespace WebApi.Services;

public class SatLoPointRepository : BaseRepository{
    public SatLoPointRepository(IDbConnection connection) : base(connection){}

    public IEnumerable<SatLoPoint> GetSatLoPoints(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        // trường hợp tìm kiếm theo từng quận huyện (truyền mã huyện)
        if (mahuyen != "null"){
            //trường hợp tìm kiếm theo từng quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<SatLoPoint>("SELECT a.objectid, a.idsatlop, a.vitri, a.tuyensong, a.capsong, a.chieudai::Numeric, a.chieurong::Numeric, a.mucdo, a.tinhtrang, a.anhhuong, a.khoangcachah, a.ditichah, a.sohoah, a.songuoiah, a.hatangah, a.congtrinhchongsl, a.chudautu, a.tenduan, a.quymoduan, a.tongmucduan, a.tiendothuchien, a.nguongoc, a.dubao,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, a.ctxdke, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape FROM SatLo_Point a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen"
                , new{
                    _mahuyen = mahuyen
                });
            }
            // trường hợp tìm kiếm theo từng quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<SatLoPoint>("SELECT * FROM GetSatLoPoints(@_mahuyen)"
            , new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
        // trường hợp tìm kiếm tất cả quận huyện (không truyền mã huyện)
        else{
            // trường hợp tìm kiếm tất cả quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<SatLoPoint>("SELECT a.objectid, a.idsatlop, a.vitri, a.tuyensong, a.capsong, a.chieudai::Numeric, a.chieurong::Numeric, a.mucdo, a.tinhtrang, a.anhhuong, a.khoangcachah, a.ditichah, a.sohoah, a.songuoiah, a.hatangah, a.congtrinhchongsl, a.chudautu, a.tenduan, a.quymoduan, a.tongmucduan, a.tiendothuchien, a.nguongoc, a.dubao,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, a.ctxdke, ST_Asgeojson(ST_Transform(a.shape, 4326)) AS shape FROM SatLo_Point a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen WHERE " + SqlQuery + "");
            }
            // trường hợp tìm kiếm tất cả quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<SatLoPoint>("SELECT * FROM GetSatLoPoints(@_mahuyen)"
            , new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }    
    }
    public IEnumerable<SatLoPointDetailStatistics> GetSatLoPointDetailStatistics(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        if (mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<SatLoPointDetailStatistics>("SELECT h.tenhuyen AS quan_huyen_tp, a.mucdo AS mucdosatlo, COUNT(a.mucdo) AS Soluongvitrisatlo, SUM(a.chieudai)::Numeric AS Tongchieudaisatlo, c.color AS mamau FROM SatLo_Point a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen LEFT JOIN Color c ON a.mucdo = c.name WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen GROUP BY h.tenhuyen, a.mucdo, c.color ORDER BY h.tenhuyen ASC, CASE WHEN a.mucdo = 'Sạt lở bình thường' THEN 1 WHEN a.mucdo = 'Sạt lở nguy hiểm' THEN 2 ELSE 3 END ASC"
                , new{
                    _mahuyen = mahuyen
                });
            }
                return connection.Query<SatLoPointDetailStatistics>("SELECT h.tenhuyen AS quan_huyen_tp, a.mucdo AS mucdosatlo, COUNT(a.mucdo) AS Soluongvitrisatlo, SUM(a.chieudai)::Numeric AS Tongchieudaisatlo, c.color AS mamau FROM SatLo_Point a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen LEFT JOIN Color c ON a.mucdo = c.name WHERE a.MaHuyen = @_mahuyen GROUP BY h.tenhuyen, a.mucdo, c.color ORDER BY h.tenhuyen ASC, CASE WHEN a.mucdo = 'Sạt lở bình thường' THEN 1 WHEN a.mucdo = 'Sạt lở nguy hiểm' THEN 2 ELSE 3 END ASC"
                , new{
                    _mahuyen = mahuyen
            });      
        }
        else{
            if (SqlQuery != "null"){
                return connection.Query<SatLoPointDetailStatistics>("SELECT h.tenhuyen AS quan_huyen_tp, a.mucdo AS mucdosatlo, COUNT(a.mucdo) AS Soluongvitrisatlo, SUM(a.chieudai)::Numeric AS Tongchieudaisatlo, c.color AS mamau FROM SatLo_Point a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen LEFT JOIN Color c ON a.mucdo = c.name WHERE " + SqlQuery + " GROUP BY h.tenhuyen, a.mucdo, c.color ORDER BY h.tenhuyen ASC, CASE WHEN a.mucdo = 'Sạt lở bình thường' THEN 1 WHEN a.mucdo = 'Sạt lở nguy hiểm' THEN 2 ELSE 3 END ASC");            
            }
            return connection.Query<SatLoPointDetailStatistics>("SELECT h.tenhuyen AS quan_huyen_tp, a.mucdo AS mucdosatlo, COUNT(a.mucdo) AS Soluongvitrisatlo, SUM(a.chieudai)::Numeric AS Tongchieudaisatlo, c.color AS mamau FROM SatLo_Point a LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen LEFT JOIN Color c ON a.mucdo = c.name GROUP BY h.tenhuyen, a.mucdo, c.color ORDER BY h.tenhuyen ASC, CASE WHEN a.mucdo = 'Sạt lở bình thường' THEN 1 WHEN a.mucdo = 'Sạt lở nguy hiểm' THEN 2 ELSE 3 END ASC");  
        }       
    } 
    public IEnumerable<SatLoPointTotalStatistics> GetSatLoPointTotalStatistics(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        if (mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<SatLoPointTotalStatistics>("(SELECT  a.mucdo AS mucdosatlo, COUNT(a.mucdo) AS Soluongvitrisatlo, SUM(a.chieudai)::Numeric AS Tongchieudaisatlo, c.color AS mamau, 'Mức độ sạt lở' AS phamvithongke FROM SatLo_Point a  LEFT JOIN Color c ON a.mucdo = c.name WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen GROUP BY  a.mucdo, c.color ORDER BY CASE WHEN a.mucdo = 'Sạt lở bình thường' THEN 1 WHEN a.mucdo = 'Sạt lở nguy hiểm' THEN 2 ELSE 3 END ASC)"
                , new{
                    _mahuyen = mahuyen
                });
            }
                return connection.Query<SatLoPointTotalStatistics>("(SELECT  a.mucdo AS mucdosatlo, COUNT(a.mucdo) AS Soluongvitrisatlo, SUM(a.chieudai)::Numeric AS Tongchieudaisatlo, c.color AS mamau, 'Mức độ sạt lở' AS phamvithongke FROM SatLo_Point a  LEFT JOIN Color c ON a.mucdo = c.name WHERE a.MaHuyen = @_mahuyen GROUP BY  a.mucdo, c.color ORDER BY CASE WHEN a.mucdo = 'Sạt lở bình thường' THEN 1 WHEN a.mucdo = 'Sạt lở nguy hiểm' THEN 2 ELSE 3 END ASC)"
                , new{
                    _mahuyen = mahuyen
            });      
        }
        else{
            if (SqlQuery != "null"){
                return connection.Query<SatLoPointTotalStatistics>("(SELECT  a.mucdo AS mucdosatlo, COUNT(a.mucdo) AS Soluongvitrisatlo, SUM(a.chieudai)::Numeric AS Tongchieudaisatlo, c.color AS mamau, 'Mức độ sạt lở' AS phamvithongke FROM SatLo_Point a  LEFT JOIN Color c ON a.mucdo = c.name WHERE " + SqlQuery + " GROUP BY  a.mucdo, c.color ORDER BY CASE WHEN a.mucdo = 'Sạt lở bình thường' THEN 1 WHEN a.mucdo = 'Sạt lở nguy hiểm' THEN 2 ELSE 3 END ASC)");            
            }
            return connection.Query<SatLoPointTotalStatistics>("(SELECT  a.mucdo AS mucdosatlo, COUNT(a.mucdo) AS Soluongvitrisatlo, SUM(a.chieudai)::Numeric AS Tongchieudaisatlo, c.color AS mamau, 'Mức độ sạt lở' AS phamvithongke FROM SatLo_Point a LEFT JOIN Color c ON a.mucdo = c.name GROUP BY  a.mucdo, c.color ORDER BY CASE WHEN a.mucdo = 'Sạt lở bình thường' THEN 1 WHEN a.mucdo = 'Sạt lở nguy hiểm' THEN 2 ELSE 3 END ASC)");  
        }         
    } 
}