using System.Data;
using Dapper;
using WebApi.Models;

namespace WebApi.Services;

public class HoChuaRepository : BaseRepository{
    public HoChuaRepository(IDbConnection connection) : base(connection){}
    public IEnumerable<HoChua> GetHoChuas(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        // trường hợp tìm kiếm theo từng quận huyện (truyền mã huyện)
        if (mahuyen != "null"){
            //trường hợp tìm kiếm theo từng quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                    return connection.Query<HoChua>("SELECT a.objectid, a.idhochua, a.ten, a.loaiho, a.vitri, ROUND(a.kinhdo::Numeric, 6) AS kinhdo, ROUND(a.vido::Numeric, 6) AS vido,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, a.h, a.w, a.qvh, a.qxa, a.qcsi, a.qcsii, a.qcsiii, a.qtb, a.bh, a.r, a.namcapnhat, a.ghichu, ST_asgeojson(ST_Transform(a.shape, 4326)) AS shape FROM HoChua a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen ORDER BY a.ngay ASC"
                    ,new{
                        _mahuyen = mahuyen
                });
            }   
            // trường hợp tìm kiếm theo từng quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<HoChua>("SELECT * FROM GetHoChuas(@_mahuyen)", new{
                _mahuyen = mahuyen  
            }, commandType: CommandType.Text);
        }
        // trường hợp tìm kiếm tất cả quận huyện (không truyền mã huyện)
        else{
            // trường hợp tìm kiếm tất cả quận huyện và có truyền điều kiện tìm kiếm
            if (SqlQuery != "null"){
                return connection.Query<HoChua>("SELECT a.objectid, a.idhochua, a.ten, a.loaiho, a.vitri, ROUND(a.kinhdo::Numeric, 6) AS kinhdo, ROUND(a.vido::Numeric, 6) AS vido,a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, a.h, a.w, a.qvh, a.qxa, a.qcsi, a.qcsii, a.qcsiii, a.qtb, a.bh, a.r, a.namcapnhat, a.ghichu, ST_asgeojson(ST_Transform(a.shape, 4326)) AS shape FROM HoChua a LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen WHERE " + SqlQuery + " ORDER BY a.ngay ASC");
            }
            // trường hợp tìm kiếm tất cả quận huyện và không truyền điều kiện tìm kiếm
            return connection.Query<HoChua>("SELECT * FROM GetHoChuas(@_mahuyen)", new{
                _mahuyen = mahuyen
            }, commandType: CommandType.Text);
        }
    } 
    public IEnumerable<ThuyHeHoChua> GetThuyHeHoChuas(){
        return connection.Query<ThuyHeHoChua>("SELECT * FROM GetThuyHeHoChuas()", commandType: CommandType.Text);        
    }
    public IEnumerable<ReservoirDetailStatistics> GetReservoirDetailStatistics(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        if (mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<ReservoirDetailStatistics>(
                    "SELECT a.nam, a.tencachochua, a.tongsophantu, a.thongso, a.luuluongthapnhat, a.thoidiemluuluongthapnhat, a.luuluongcaonhat, a.thoidiemluuluongcaonhat, luuluongtrungbinh FROM ( (SELECT x.year AS nam, z.ten AS tencachochua, z.Tongsophantu, 'Mực nước'::Text AS Thongso, x.h AS luuluongthapnhat, x.ngay AS thoidiemluuluongthapnhat, y.h AS luuluongcaonhat, y.ngay AS thoidiemluuluongcaonhat, z.mucnuoctrungbinh AS luuluongtrungbinh FROM ( SELECT a.year, a.min AS h, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM  ( SELECT a.year, MIN(a.h) from HoChua a WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen AND a.h != 0 GROUP BY a.year ) a LEFT JOIN HoChua b ON b.year = a.year AND a.min = b.h GROUP BY a.year, a.min ) x, ( SELECT a.year, a.max AS h, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM  ( SELECT a.year, MAX(a.h) from HoChua a WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen AND a.h != 0 GROUP BY a.year ) a LEFT JOIN HoChua b ON b.year = a.year AND a.max = b.h GROUP BY a.year, a.max ) y, ( SELECT a.year, STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.h)::Numeric, 2) AS mucnuoctrungbinh	FROM HoChua a WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen AND a.h != 0 GROUP BY a.year) z WHERE x.year = y.year AND y.year = z.year ORDER BY x.year ASC) UNION ALL (SELECT x.year AS nam, z.ten AS tencachochua, z.Tongsophantu, 'Lưu lượng nước về hồ'::Text AS Thongso, x.min AS luuluongthapnhat, x.ngay AS thoidiemluuluongthapnhat, y.max AS luuluongcaonhat, y.ngay AS thoidiemluuluongcaonhat, z.mucnuoctrungbinh AS luuluongtrungbinh FROM ( SELECT a.year, a.min, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM  ( SELECT a.year, MIN(a.qvh) from HoChua a WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen AND a.qvh != 0 GROUP BY a.year ) a LEFT JOIN HoChua b ON b.year = a.year AND a.min = b.qvh GROUP BY a.year, a.min ) x, ( SELECT a.year, a.max, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM ( SELECT a.year, MAX(a.qvh) from HoChua a WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen AND a.qvh != 0 GROUP BY a.year ) a LEFT JOIN HoChua b ON b.year = a.year AND a.max = b.qvh GROUP BY a.year, a.max ) y, ( SELECT a.year, STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.qvh)::Numeric, 2) AS mucnuoctrungbinh FROM HoChua a WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen AND a.qvh != 0 GROUP BY a.year ) z WHERE x.year = y.year AND y.year = z.year ORDER BY x.year ASC) UNION ALL (SELECT x.year AS nam, z.ten AS tencachochua, z.Tongsophantu, 'Lưu lượng nước xã tràn'::Text AS Thongso, x.min AS luuluongthapnhat, x.ngay AS thoidiemluuluongthapnhat, y.max AS luuluongcaonhat, y.ngay AS thoidiemluuluongcaonhat, z.mucnuoctrungbinh AS luuluongtrungbinh FROM ( SELECT a.year, a.min, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM ( SELECT a.year, MIN(a.qxa) from HoChua a WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen AND a.qxa != 0 GROUP BY a.year ) a LEFT JOIN HoChua b ON b.year = a.year AND a.min = b.qxa GROUP BY a.year, a.min ) x, ( SELECT a.year, a.max, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM  ( SELECT a.year, MAX(a.qxa) from HoChua a WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen AND a.qxa != 0 GROUP BY a.year ) a LEFT JOIN HoChua b ON b.year = a.year AND a.max = b.qxa GROUP BY a.year, a.max ) y, ( SELECT a.year, STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.qxa)::Numeric, 2) AS mucnuoctrungbinh FROM HoChua a WHERE " + SqlQuery + " AND a.MaHuyen = @_mahuyen AND a.qxa != 0 GROUP BY a.year ) z WHERE x.year = y.year AND y.year = z.year ORDER BY x.year ASC) ) a ORDER BY a.nam ASC, a.tencachochua ASC"         
                    , new{
                    _mahuyen = mahuyen
                });
            }
            return connection.Query<ReservoirDetailStatistics>(
                "SELECT a.nam, a.tencachochua, a.tongsophantu, a.thongso, a.luuluongthapnhat, a.thoidiemluuluongthapnhat, a.luuluongcaonhat, a.thoidiemluuluongcaonhat, luuluongtrungbinh FROM ( (SELECT x.year AS nam, z.ten AS tencachochua, z.Tongsophantu, 'Mực nước'::Text AS Thongso, x.h AS luuluongthapnhat, x.ngay AS thoidiemluuluongthapnhat, y.h AS luuluongcaonhat, y.ngay AS thoidiemluuluongcaonhat, z.mucnuoctrungbinh AS luuluongtrungbinh FROM ( SELECT a.year, a.min AS h, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM  ( SELECT a.year, MIN(a.h) from HoChua a WHERE a.MaHuyen = @_mahuyen AND a.h != 0 GROUP BY a.year ) a LEFT JOIN HoChua b ON b.year = a.year AND a.min = b.h GROUP BY a.year, a.min ) x, ( SELECT a.year, a.max AS h, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM  ( SELECT a.year, MAX(a.h) from HoChua a WHERE a.MaHuyen = @_mahuyen AND a.h != 0 GROUP BY a.year ) a LEFT JOIN HoChua b ON b.year = a.year AND a.max = b.h GROUP BY a.year, a.max ) y, ( SELECT a.year, STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.h)::Numeric, 2) AS mucnuoctrungbinh	FROM HoChua a WHERE a.MaHuyen = @_mahuyen AND a.h != 0 GROUP BY a.year) z WHERE x.year = y.year AND y.year = z.year ORDER BY x.year ASC) UNION ALL (SELECT x.year AS nam, z.ten AS tencachochua, z.Tongsophantu, 'Lưu lượng nước về hồ'::Text AS Thongso, x.min AS luuluongthapnhat, x.ngay AS thoidiemluuluongthapnhat, y.max AS luuluongcaonhat, y.ngay AS thoidiemluuluongcaonhat, z.mucnuoctrungbinh AS luuluongtrungbinh FROM ( SELECT a.year, a.min, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM  ( SELECT a.year, MIN(a.qvh) from HoChua a WHERE a.MaHuyen = @_mahuyen AND a.qvh != 0 GROUP BY a.year ) a LEFT JOIN HoChua b ON b.year = a.year AND a.min = b.qvh GROUP BY a.year, a.min ) x, ( SELECT a.year, a.max, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM ( SELECT a.year, MAX(a.qvh) from HoChua a WHERE a.MaHuyen = @_mahuyen AND a.qvh != 0 GROUP BY a.year ) a LEFT JOIN HoChua b ON b.year = a.year AND a.max = b.qvh GROUP BY a.year, a.max ) y, ( SELECT a.year, STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.qvh)::Numeric, 2) AS mucnuoctrungbinh FROM HoChua a WHERE a.MaHuyen = @_mahuyen AND a.qvh != 0 GROUP BY a.year ) z WHERE x.year = y.year AND y.year = z.year ORDER BY x.year ASC) UNION ALL (SELECT x.year AS nam, z.ten AS tencachochua, z.Tongsophantu, 'Lưu lượng nước xã tràn'::Text AS Thongso, x.min AS luuluongthapnhat, x.ngay AS thoidiemluuluongthapnhat, y.max AS luuluongcaonhat, y.ngay AS thoidiemluuluongcaonhat, z.mucnuoctrungbinh AS luuluongtrungbinh FROM ( SELECT a.year, a.min, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM ( SELECT a.year, MIN(a.qxa) from HoChua a WHERE a.MaHuyen = @_mahuyen AND a.qxa != 0 GROUP BY a.year ) a LEFT JOIN HoChua b ON b.year = a.year AND a.min = b.qxa GROUP BY a.year, a.min ) x, ( SELECT a.year, a.max, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM  ( SELECT a.year, MAX(a.qxa) from HoChua a WHERE a.MaHuyen = @_mahuyen AND a.qxa != 0 GROUP BY a.year ) a LEFT JOIN HoChua b ON b.year = a.year AND a.max = b.qxa GROUP BY a.year, a.max ) y, ( SELECT a.year, STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.qxa)::Numeric, 2) AS mucnuoctrungbinh FROM HoChua a WHERE a.MaHuyen = @_mahuyen AND a.qxa != 0 GROUP BY a.year ) z WHERE x.year = y.year AND y.year = z.year ORDER BY x.year ASC) ) a ORDER BY a.nam ASC, a.tencachochua ASC"           
                , new{
                _mahuyen = mahuyen
            });      
        }
        else{
            if (SqlQuery != "null"){            
                return connection.Query<ReservoirDetailStatistics>(
                "SELECT a.nam, a.tencachochua, a.tongsophantu, a.thongso, a.luuluongthapnhat, a.thoidiemluuluongthapnhat, a.luuluongcaonhat, a.thoidiemluuluongcaonhat, luuluongtrungbinh FROM ( (SELECT x.year AS nam, z.ten AS tencachochua, z.Tongsophantu, 'Mực nước'::Text AS Thongso, x.h AS luuluongthapnhat, x.ngay AS thoidiemluuluongthapnhat, y.h AS luuluongcaonhat, y.ngay AS thoidiemluuluongcaonhat, z.mucnuoctrungbinh AS luuluongtrungbinh FROM ( SELECT a.year, a.min AS h, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM  ( SELECT a.year, MIN(a.h) from HoChua a WHERE " + SqlQuery + " AND a.h != 0 GROUP BY a.year ) a LEFT JOIN HoChua b ON b.year = a.year AND a.min = b.h GROUP BY a.year, a.min ) x, ( SELECT a.year, a.max AS h, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM  ( SELECT a.year, MAX(a.h) from HoChua a WHERE " + SqlQuery + " AND a.h != 0 GROUP BY a.year ) a LEFT JOIN HoChua b ON b.year = a.year AND a.max = b.h GROUP BY a.year, a.max ) y, ( SELECT a.year, STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.h)::Numeric, 2) AS mucnuoctrungbinh	FROM HoChua a WHERE " + SqlQuery + " AND a.h != 0 GROUP BY a.year) z WHERE x.year = y.year AND y.year = z.year ORDER BY x.year ASC) UNION ALL (SELECT x.year AS nam, z.ten AS tencachochua, z.Tongsophantu, 'Lưu lượng nước về hồ'::Text AS Thongso, x.min AS luuluongthapnhat, x.ngay AS thoidiemluuluongthapnhat, y.max AS luuluongcaonhat, y.ngay AS thoidiemluuluongcaonhat, z.mucnuoctrungbinh AS luuluongtrungbinh FROM ( SELECT a.year, a.min, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM  ( SELECT a.year, MIN(a.qvh) from HoChua a WHERE " + SqlQuery + " AND a.qvh != 0 GROUP BY a.year ) a LEFT JOIN HoChua b ON b.year = a.year AND a.min = b.qvh GROUP BY a.year, a.min ) x, ( SELECT a.year, a.max, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM ( SELECT a.year, MAX(a.qvh) from HoChua a WHERE " + SqlQuery + " AND a.qvh != 0 GROUP BY a.year ) a LEFT JOIN HoChua b ON b.year = a.year AND a.max = b.qvh GROUP BY a.year, a.max ) y, ( SELECT a.year, STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.qvh)::Numeric, 2) AS mucnuoctrungbinh FROM HoChua a WHERE " + SqlQuery + " AND a.qvh != 0 GROUP BY a.year ) z WHERE x.year = y.year AND y.year = z.year ORDER BY x.year ASC) UNION ALL (SELECT x.year AS nam, z.ten AS tencachochua, z.Tongsophantu, 'Lưu lượng nước xã tràn'::Text AS Thongso, x.min AS luuluongthapnhat, x.ngay AS thoidiemluuluongthapnhat, y.max AS luuluongcaonhat, y.ngay AS thoidiemluuluongcaonhat, z.mucnuoctrungbinh AS luuluongtrungbinh FROM ( SELECT a.year, a.min, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM ( SELECT a.year, MIN(a.qxa) from HoChua a WHERE " + SqlQuery + " AND a.qxa != 0 GROUP BY a.year ) a LEFT JOIN HoChua b ON b.year = a.year AND a.min = b.qxa GROUP BY a.year, a.min ) x, ( SELECT a.year, a.max, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM  ( SELECT a.year, MAX(a.qxa) from HoChua a WHERE " + SqlQuery + " AND a.qxa != 0 GROUP BY a.year ) a LEFT JOIN HoChua b ON b.year = a.year AND a.max = b.qxa GROUP BY a.year, a.max ) y, ( SELECT a.year, STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.qxa)::Numeric, 2) AS mucnuoctrungbinh FROM HoChua a WHERE " + SqlQuery + " AND a.qxa != 0 GROUP BY a.year ) z WHERE x.year = y.year AND y.year = z.year ORDER BY x.year ASC) ) a ORDER BY a.nam ASC, a.tencachochua ASC");            
            }
            return connection.Query<ReservoirDetailStatistics>(
            "SELECT a.nam, a.tencachochua, a.tongsophantu, a.thongso, a.luuluongthapnhat, a.thoidiemluuluongthapnhat, a.luuluongcaonhat, a.thoidiemluuluongcaonhat, luuluongtrungbinh FROM ( (SELECT x.year AS nam, z.ten AS tencachochua, z.Tongsophantu, 'Mực nước'::Text AS Thongso, x.h AS luuluongthapnhat, x.ngay AS thoidiemluuluongthapnhat, y.h AS luuluongcaonhat, y.ngay AS thoidiemluuluongcaonhat, z.mucnuoctrungbinh AS luuluongtrungbinh FROM ( SELECT a.year, a.min AS h, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM  ( SELECT a.year, MIN(a.h) from HoChua a WHERE a.h != 0 GROUP BY a.year ) a LEFT JOIN HoChua b ON b.year = a.year AND a.min = b.h GROUP BY a.year, a.min ) x, ( SELECT a.year, a.max AS h, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM  ( SELECT a.year, MAX(a.h) from HoChua a WHERE a.h != 0 GROUP BY a.year ) a LEFT JOIN HoChua b ON b.year = a.year AND a.max = b.h GROUP BY a.year, a.max ) y, ( SELECT a.year, STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.h)::Numeric, 2) AS mucnuoctrungbinh	FROM HoChua a WHERE a.h != 0 GROUP BY a.year) z WHERE x.year = y.year AND y.year = z.year ORDER BY x.year ASC) UNION ALL (SELECT x.year AS nam, z.ten AS tencachochua, z.Tongsophantu, 'Lưu lượng nước về hồ'::Text AS Thongso, x.min AS luuluongthapnhat, x.ngay AS thoidiemluuluongthapnhat, y.max AS luuluongcaonhat, y.ngay AS thoidiemluuluongcaonhat, z.mucnuoctrungbinh AS luuluongtrungbinh FROM ( SELECT a.year, a.min, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM  ( SELECT a.year, MIN(a.qvh) from HoChua a WHERE a.qvh != 0 GROUP BY a.year ) a LEFT JOIN HoChua b ON b.year = a.year AND a.min = b.qvh GROUP BY a.year, a.min ) x, ( SELECT a.year, a.max, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM ( SELECT a.year, MAX(a.qvh) from HoChua a WHERE a.qvh != 0 GROUP BY a.year ) a LEFT JOIN HoChua b ON b.year = a.year AND a.max = b.qvh GROUP BY a.year, a.max ) y, ( SELECT a.year, STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.qvh)::Numeric, 2) AS mucnuoctrungbinh FROM HoChua a WHERE a.qvh != 0 GROUP BY a.year ) z WHERE x.year = y.year AND y.year = z.year ORDER BY x.year ASC) UNION ALL (SELECT x.year AS nam, z.ten AS tencachochua, z.Tongsophantu, 'Lưu lượng nước xã tràn'::Text AS Thongso, x.min AS luuluongthapnhat, x.ngay AS thoidiemluuluongthapnhat, y.max AS luuluongcaonhat, y.ngay AS thoidiemluuluongcaonhat, z.mucnuoctrungbinh AS luuluongtrungbinh FROM ( SELECT a.year, a.min, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM ( SELECT a.year, MIN(a.qxa) from HoChua a WHERE a.qxa != 0 GROUP BY a.year ) a LEFT JOIN HoChua b ON b.year = a.year AND a.min = b.qxa GROUP BY a.year, a.min ) x, ( SELECT a.year, a.max, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay FROM  ( SELECT a.year, MAX(a.qxa) from HoChua a WHERE a.qxa != 0 GROUP BY a.year ) a LEFT JOIN HoChua b ON b.year = a.year AND a.max = b.qxa GROUP BY a.year, a.max ) y, ( SELECT a.year, STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.qxa)::Numeric, 2) AS mucnuoctrungbinh FROM HoChua a WHERE a.qxa != 0 GROUP BY a.year ) z WHERE x.year = y.year AND y.year = z.year ORDER BY x.year ASC) ) a ORDER BY a.nam ASC, a.tencachochua ASC"); 
        }
    }       
    public IEnumerable<ReservoirTotalStatistics> GetReservoirTotalStatistics(string mahuyen, string? SqlQuery){
        if (SqlQuery!.Contains("SELECT") || SqlQuery.Contains("select") || SqlQuery.Contains("PG_SLEEP") || SqlQuery.Contains("pg_sleep") || SqlQuery.Contains("now()") || SqlQuery.Contains("NOW()") || SqlQuery.Contains("CURRENT_TIME()") || SqlQuery.Contains("current_time()") || SqlQuery.Contains("--") || SqlQuery.Contains("UNION") || SqlQuery.Contains("union") || SqlQuery.Contains("INSERT") || SqlQuery.Contains("insert") || SqlQuery.Contains("UPDATE") || SqlQuery.Contains("update") || SqlQuery.Contains("DELETE") || SqlQuery.Contains("delete") || SqlQuery.Contains("TRUNCATE") || SqlQuery.Contains("truncate") || SqlQuery.Contains("ALTER") || SqlQuery.Contains("alter") || SqlQuery.Contains("ADD") || SqlQuery.Contains("add") || SqlQuery.Contains("CREATE") || SqlQuery.Contains("create") || SqlQuery.Contains("DROP") || SqlQuery.Contains("drop") || SqlQuery.Contains("RENAME") || SqlQuery.Contains("rename") || SqlQuery.Contains("DECLARE") || SqlQuery.Contains("declare")){
            return null!;
        }
        if (mahuyen != "null"){
            if (SqlQuery != "null"){
                return connection.Query<ReservoirTotalStatistics>(
                    "SELECT z.ten AS tencachochua, z.Tongsophantu, 'Mực nước'::Text AS Thongso, x.h AS luuluongthapnhat, x.ngay AS thoidiemluuluongthapnhat, y.h AS luuluongcaonhat, y.ngay AS thoidiemluuluongcaonhat, z.mucnuoctrungbinh AS luuluongtrungbinh FROM(SELECT a.h, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM HoChua a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.h IN	(SELECT MIN(a.h) from HoChua a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.h != 0) GROUP BY a.h) x,(	SELECT a.h, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM HoChua a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.h IN (SELECT MAX(a.h) from HoChua a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.h != 0) GROUP BY a.h) y, (SELECT STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.h)::Numeric, 2) AS mucnuoctrungbinh FROM HoChua a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.h != 0 ) z UNION ALL SELECT z.ten AS Ten, z.Tongsophantu, 'Lưu lượng nước về hồ'::Text AS Thongso, x.qvh AS thapnhat, x.ngay AS thoidiemluuluongthapnhat, y.qvh AS caonhat, y.ngay AS thoidiemluuluongcaonhat, z.vehotrungbinh AS luuluongtrungbinh FROM (SELECT a.qvh, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM HoChua a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.qvh IN(SELECT MIN(a.qvh) from HoChua a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.qvh != 0)GROUP BY a.qvh) x,(SELECT a.qvh, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM HoChua a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.qvh IN (SELECT MAX(a.qvh) from HoChua a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.qvh != 0) GROUP BY a.qvh) y, (SELECT STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.qvh)::Numeric, 2) AS vehotrungbinh	FROM HoChua a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.qvh != 0 ) z UNION ALL SELECT z.ten AS Ten, z.Tongsophantu, 'Lưu lượng nước xã tràn'::Text AS Thongso, x.qxa AS thapnhat, x.ngay AS thoidiemluuluongthapnhat, y.qxa AS caonhat, y.ngay AS thoidiemluuluongcaonhat, z.xatrungbinh AS luuluongtrungbinh FROM (SELECT a.qxa, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM HoChua a WHERE  " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.qxa IN (SELECT MIN(a.qxa) from HoChua a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.qxa != 0) GROUP BY a.qxa) x,(SELECT a.qxa, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM HoChua a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.qxa IN (SELECT MAX(a.qxa) from HoChua a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.qxa != 0) GROUP BY a.qxa) y, (SELECT STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.qxa)::Numeric, 2) AS xatrungbinh FROM HoChua a WHERE " + SqlQuery + " AND a.mahuyen = @_mahuyen AND a.qxa != 0 ) z"         
                    , new{
                    _mahuyen = mahuyen
                });
            }
            return connection.Query<ReservoirTotalStatistics>(
                "SELECT z.ten AS tencachochua, z.Tongsophantu, 'Mực nước'::Text AS Thongso, x.h AS luuluongthapnhat, x.ngay AS thoidiemluuluongthapnhat, y.h AS luuluongcaonhat, y.ngay AS thoidiemluuluongcaonhat, z.mucnuoctrungbinh AS luuluongtrungbinh FROM(SELECT a.h, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM HoChua a WHERE mahuyen = @_mahuyen AND a.h IN	(SELECT MIN(a.h) from HoChua a WHERE mahuyen = @_mahuyen AND a.h != 0) GROUP BY a.h) x,(	SELECT a.h, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM HoChua a WHERE mahuyen = @_mahuyen AND a.h IN (SELECT MAX(a.h) from HoChua a WHERE mahuyen = @_mahuyen AND a.h != 0) GROUP BY a.h) y, (SELECT STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.h)::Numeric, 2) AS mucnuoctrungbinh FROM HoChua a WHERE mahuyen = @_mahuyen AND a.h != 0 ) z UNION ALL SELECT z.ten AS Ten, z.Tongsophantu, 'Lưu lượng nước về hồ'::Text AS Thongso, x.qvh AS thapnhat, x.ngay AS thoidiemluuluongthapnhat, y.qvh AS caonhat, y.ngay AS thoidiemluuluongcaonhat, z.vehotrungbinh AS luuluongtrungbinh FROM (SELECT a.qvh, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM HoChua a WHERE mahuyen = @_mahuyen AND a.qvh IN(SELECT MIN(a.qvh) from HoChua a WHERE mahuyen = @_mahuyen AND a.qvh != 0)GROUP BY a.qvh) x,(SELECT a.qvh, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM HoChua a WHERE mahuyen = @_mahuyen AND a.qvh IN (SELECT MAX(a.qvh) from HoChua a WHERE mahuyen = @_mahuyen AND a.qvh != 0) GROUP BY a.qvh) y, (SELECT STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.qvh)::Numeric, 2) AS vehotrungbinh	FROM HoChua a WHERE mahuyen = @_mahuyen AND a.qvh != 0 ) z UNION ALL SELECT z.ten AS Ten, z.Tongsophantu, 'Lưu lượng nước xã tràn'::Text AS Thongso, x.qxa AS thapnhat, x.ngay AS thoidiemluuluongthapnhat, y.qxa AS caonhat, y.ngay AS thoidiemluuluongcaonhat, z.xatrungbinh AS luuluongtrungbinh FROM (SELECT a.qxa, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM HoChua a WHERE  mahuyen = @_mahuyen AND a.qxa IN (SELECT MIN(a.qxa) from HoChua a WHERE mahuyen = @_mahuyen AND a.qxa != 0) GROUP BY a.qxa) x,(SELECT a.qxa, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM HoChua a WHERE mahuyen = @_mahuyen AND a.qxa IN (SELECT MAX(a.qxa) from HoChua a WHERE mahuyen = @_mahuyen AND a.qxa != 0) GROUP BY a.qxa) y, (SELECT STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.qxa)::Numeric, 2) AS xatrungbinh FROM HoChua a WHERE mahuyen = @_mahuyen AND a.qxa != 0 ) z"           
                , new{
                _mahuyen = mahuyen
            });      
        }
        else{
            if (SqlQuery != "null"){            
                return connection.Query<ReservoirTotalStatistics>(
                "SELECT z.ten AS tencachochua, z.Tongsophantu, 'Mực nước'::Text AS Thongso, x.h AS luuluongthapnhat, x.ngay AS thoidiemluuluongthapnhat, y.h AS luuluongcaonhat, y.ngay AS thoidiemluuluongcaonhat, z.mucnuoctrungbinh AS luuluongtrungbinh FROM(SELECT a.h, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM HoChua a WHERE " + SqlQuery + " AND a.h IN	(SELECT MIN(a.h) from HoChua a WHERE " + SqlQuery + " AND a.h != 0) GROUP BY a.h) x,(	SELECT a.h, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM HoChua a WHERE " + SqlQuery + " AND a.h IN (SELECT MAX(a.h) from HoChua a WHERE " + SqlQuery + " AND a.h != 0) GROUP BY a.h) y, (SELECT STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.h)::Numeric, 2) AS mucnuoctrungbinh FROM HoChua a WHERE " + SqlQuery + " AND a.h != 0 ) z UNION ALL SELECT z.ten AS Ten, z.Tongsophantu, 'Lưu lượng nước về hồ'::Text AS Thongso, x.qvh AS thapnhat, x.ngay AS thoidiemluuluongthapnhat, y.qvh AS caonhat, y.ngay AS thoidiemluuluongcaonhat, z.vehotrungbinh AS luuluongtrungbinh FROM (SELECT a.qvh, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM HoChua a WHERE " + SqlQuery + " AND a.qvh IN(SELECT MIN(a.qvh) from HoChua a WHERE " + SqlQuery + " AND a.qvh != 0)GROUP BY a.qvh) x,(SELECT a.qvh, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM HoChua a WHERE " + SqlQuery + " AND a.qvh IN (SELECT MAX(a.qvh) from HoChua a WHERE " + SqlQuery + " AND a.qvh != 0) GROUP BY a.qvh) y, (SELECT STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.qvh)::Numeric, 2) AS vehotrungbinh	FROM HoChua a WHERE " + SqlQuery + " AND a.qvh != 0 ) z UNION ALL SELECT z.ten AS Ten, z.Tongsophantu, 'Lưu lượng nước xã tràn'::Text AS Thongso, x.qxa AS thapnhat, x.ngay AS thoidiemluuluongthapnhat, y.qxa AS caonhat, y.ngay AS thoidiemluuluongcaonhat, z.xatrungbinh AS luuluongtrungbinh FROM (SELECT a.qxa, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM HoChua a WHERE  " + SqlQuery + " AND a.qxa IN (SELECT MIN(a.qxa) from HoChua a WHERE " + SqlQuery + " AND a.qxa != 0) GROUP BY a.qxa) x,(SELECT a.qxa, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM HoChua a WHERE " + SqlQuery + " AND a.qxa IN (SELECT MAX(a.qxa) from HoChua a WHERE " + SqlQuery + " AND a.qxa != 0) GROUP BY a.qxa) y, (SELECT STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.qxa)::Numeric, 2) AS xatrungbinh FROM HoChua a WHERE " + SqlQuery + " AND a.qxa != 0 ) z");            
            }
            return connection.Query<ReservoirTotalStatistics>(
            "SELECT z.ten AS tencachochua, z.Tongsophantu, 'Mực nước'::Text AS Thongso, x.h AS luuluongthapnhat, x.ngay AS thoidiemluuluongthapnhat, y.h AS luuluongcaonhat, y.ngay AS thoidiemluuluongcaonhat, z.mucnuoctrungbinh AS luuluongtrungbinh FROM(SELECT a.h, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM HoChua a WHERE a.h IN	(SELECT MIN(a.h) from HoChua a WHERE a.h != 0) GROUP BY a.h) x,(	SELECT a.h, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM HoChua a WHERE a.h IN (SELECT MAX(a.h) from HoChua a WHERE a.h != 0) GROUP BY a.h) y, (SELECT STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.h)::Numeric, 2) AS mucnuoctrungbinh FROM HoChua a WHERE a.h != 0 ) z UNION ALL SELECT z.ten AS Ten, z.Tongsophantu, 'Lưu lượng nước về hồ'::Text AS Thongso, x.qvh AS thapnhat, x.ngay AS thoidiemluuluongthapnhat, y.qvh AS caonhat, y.ngay AS thoidiemluuluongcaonhat, z.vehotrungbinh AS luuluongtrungbinh FROM (SELECT a.qvh, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM HoChua a WHERE a.qvh IN(SELECT MIN(a.qvh) from HoChua a WHERE a.qvh != 0)GROUP BY a.qvh) x,(SELECT a.qvh, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM HoChua a WHERE a.qvh IN (SELECT MAX(a.qvh) from HoChua a WHERE a.qvh != 0) GROUP BY a.qvh) y, (SELECT STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.qvh)::Numeric, 2) AS vehotrungbinh	FROM HoChua a WHERE a.qvh != 0 ) z UNION ALL SELECT z.ten AS Ten, z.Tongsophantu, 'Lưu lượng nước xã tràn'::Text AS Thongso, x.qxa AS thapnhat, x.ngay AS thoidiemluuluongthapnhat, y.qxa AS caonhat, y.ngay AS thoidiemluuluongcaonhat, z.xatrungbinh AS luuluongtrungbinh FROM (SELECT a.qxa, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM HoChua a WHERE  a.qxa IN (SELECT MIN(a.qxa) from HoChua a WHERE a.qxa != 0) GROUP BY a.qxa) x,(SELECT a.qxa, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay FROM HoChua a WHERE a.qxa IN (SELECT MAX(a.qxa) from HoChua a WHERE a.qxa != 0) GROUP BY a.qxa) y, (SELECT STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.qxa)::Numeric, 2) AS xatrungbinh FROM HoChua a WHERE a.qxa != 0 ) z"); 
        }
    }   
    public HoChua? GetHoChua(int objectid){
        return connection.QueryFirstOrDefault<HoChua>("SELECT * FROM GetHoChua(@_objectid)", new{
            _objectid = objectid
        });
    }
    public int GetMaxObjectId(){
        return connection.QueryFirstOrDefault<int>("SELECT MAX(ObjectId) FROM HoChua", commandType: CommandType.Text);
    }   
    public int Add(HoChua obj){  
        DateTime ngay = Convert.ToDateTime(obj.ngay);
        double? nulltoado = null;
        string? nullstring = null!;
        double? h = obj.h == null ? null : Convert.ToDouble(obj.h);
        double? w = obj.w == null ? null : Convert.ToDouble(obj.w);
        double? qvh = obj.qvh == null ? null : Convert.ToDouble(obj.qvh);  
        double? qxa = obj.qxa == null ? null : Convert.ToDouble(obj.qxa);
        double? qcsi = obj.qcsi == null ? null : Convert.ToDouble(obj.qcsi);
        double? qcsii = obj.qcsii == null ? null : Convert.ToDouble(obj.qcsii);
        double? qcsiii = obj.qcsii == null ? null : Convert.ToDouble(obj.qcsiii);
        double? qtb = obj.qtb == null ? null : Convert.ToDouble(obj.qtb);
        double? bh = obj.bh == null ? null : Convert.ToDouble(obj.bh);
        double? r = obj.r == null ? null : Convert.ToDouble(obj.r);
        int? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt32(obj.namcapnhat);
        double? kinhdo = obj.kinhdo == null ? null : Convert.ToDouble(obj.kinhdo);
        double? vido = obj.vido == null ? null : Convert.ToDouble(obj.vido);
        
        if (obj.ngay != null){
            string[] date = obj.ngay.ToString().Split('-');
            if (obj.kinhdo != null && obj.vido != null){
                return connection.ExecuteScalar<int>("AddHoChua", 
                    new{
                        _objectid = obj.objectid,
                        _idhochua =  "HC" +  obj.objectid.ToString(),
                        _ten = obj.ten,
                        _loaiho = obj.loaiho,
                        _vitri = obj.vitri,
                        _kinhdo = Convert.ToDouble(Math.Round(Convert.ToDecimal(kinhdo), 6)),
                        _vido = Convert.ToDouble(Math.Round(Convert.ToDecimal(vido), 6)),
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _ngay = ngay,
                        _h = h,
                        _w = w,
                        _qvh = qvh,
                        _qxa = qxa,
                        _qcsi = qcsi,
                        _qcsii = qcsii,
                        _qcsiii = qcsiii,
                        _qtb = qtb,
                        _bh = bh,
                        _r = r,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _day = Convert.ToInt32(date[2]),
                        _month = Convert.ToInt32(date[1]),
                        _year = Convert.ToInt32(date[0])
                    }, commandType: CommandType.StoredProcedure
                );
            }
            if (obj.kinhdo == null && obj.vido == null){
                return connection.ExecuteScalar<int>("AddHoChua", 
                    new{
                        _objectid = obj.objectid,
                        _idhochua =  "HC" +  obj.objectid.ToString(),
                        _ten = obj.ten,
                        _loaiho = obj.loaiho,
                        _vitri = obj.vitri,
                        _kinhdo = nulltoado,
                        _vido = nulltoado,
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _ngay = ngay,
                        _h = h,
                        _w = w,
                        _qvh = qvh,
                        _qxa = qxa,
                        _qcsi = qcsi,
                        _qcsii = qcsii,
                        _qcsiii = qcsiii,
                        _qtb = qtb,
                        _bh = bh,
                        _r = r,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _day = Convert.ToInt32(date[2]),
                        _month = Convert.ToInt32(date[1]),
                        _year = Convert.ToInt32(date[0])
                    }, commandType: CommandType.StoredProcedure
                );
            }
        }
        if (obj.ngay == null){
            int? nullDMY = null;
            DateTime? ngaynull = null;
            if (obj.kinhdo != null && obj.vido != null){
                return connection.ExecuteScalar<int>("AddHoChua", 
                    new{
                        _objectid = obj.objectid,
                        _idhochua =  "HC" +  obj.objectid.ToString(),
                        _ten = obj.ten,
                        _loaiho = obj.loaiho,
                        _vitri = obj.vitri,
                        _kinhdo = Convert.ToDouble(Math.Round(Convert.ToDecimal(kinhdo), 6)),
                        _vido = Convert.ToDouble(Math.Round(Convert.ToDecimal(vido), 6)),
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _ngay = ngaynull,
                        _h = h,
                        _w = w,
                        _qvh = qvh,
                        _qxa = qxa,
                        _qcsi = qcsi,
                        _qcsii = qcsii,
                        _qcsiii = qcsiii,
                        _qtb = qtb,
                        _bh = bh,
                        _r = r,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _day = nullDMY,
                        _month = nullDMY,
                        _year = nullDMY
                    }, commandType: CommandType.StoredProcedure
                );
            }
            if (obj.kinhdo == null && obj.vido == null){
                return connection.ExecuteScalar<int>("AddHoChua", 
                    new{
                        _objectid = obj.objectid,
                        _idhochua =  "HC" +  obj.objectid.ToString(),
                        _ten = obj.ten,
                        _loaiho = obj.loaiho,
                        _vitri = obj.vitri,
                        _kinhdo = nulltoado,
                        _vido = nulltoado,
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _ngay = ngaynull,
                        _h = h,
                        _w = w,
                        _qvh = qvh,
                        _qxa = qxa,
                        _qcsi = qcsi,
                        _qcsii = qcsii,
                        _qcsiii = qcsiii,
                        _qtb = qtb,
                        _bh = bh,
                        _r = r,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _day = nullDMY,
                        _month = nullDMY,
                        _year = nullDMY
                    }, commandType: CommandType.StoredProcedure
                );
            }
        }
        return 1;
    } 
    public int Edit(int objectid, HoChua obj){     
        DateTime ngay = Convert.ToDateTime(obj.ngay);
        double? nulltoado = null;
        string? nullstring = null!;
        double? h = obj.h == null ? null : Convert.ToDouble(obj.h);
        double? w = obj.w == null ? null : Convert.ToDouble(obj.w);
        double? qvh = obj.qvh == null ? null : Convert.ToDouble(obj.qvh);  
        double? qxa = obj.qxa == null ? null : Convert.ToDouble(obj.qxa);
        double? qcsi = obj.qcsi == null ? null : Convert.ToDouble(obj.qcsi);
        double? qcsii = obj.qcsii == null ? null : Convert.ToDouble(obj.qcsii);
        double? qcsiii = obj.qcsii == null ? null : Convert.ToDouble(obj.qcsiii);
        double? qtb = obj.qtb == null ? null : Convert.ToDouble(obj.qtb);
        double? bh = obj.bh == null ? null : Convert.ToDouble(obj.bh);
        double? r = obj.r == null ? null : Convert.ToDouble(obj.r);
        int? namcapnhat = obj.namcapnhat == null ? null : Convert.ToInt32(obj.namcapnhat);
        double? kinhdo = obj.kinhdo == null ? null : Convert.ToDouble(obj.kinhdo);
        double? vido = obj.vido == null ? null : Convert.ToDouble(obj.vido);
        
        if (obj.ngay != null){
            string[] date = obj.ngay.ToString().Split('-');
            if (obj.kinhdo != null && obj.vido != null){
                return connection.ExecuteScalar<int>("EditHoChua", 
                    new{
                        _objectid = objectid,
                        _ten = obj.ten,
                        _loaiho = obj.loaiho,
                        _vitri = obj.vitri,
                        _kinhdo = Convert.ToDouble(Math.Round(Convert.ToDecimal(kinhdo), 6)),
                        _vido = Convert.ToDouble(Math.Round(Convert.ToDecimal(vido), 6)),
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _ngay = ngay,
                        _h = h,
                        _w = w,
                        _qvh = qvh,
                        _qxa = qxa,
                        _qcsi = qcsi,
                        _qcsii = qcsii,
                        _qcsiii = qcsiii,
                        _qtb = qtb,
                        _bh = bh,
                        _r = r,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _day = Convert.ToInt32(date[2]),
                        _month = Convert.ToInt32(date[1]),
                        _year = Convert.ToInt32(date[0])
                    }, commandType: CommandType.StoredProcedure
                );
            }
            if (obj.kinhdo == null && obj.vido == null){
                return connection.ExecuteScalar<int>("EditHoChua", 
                    new{
                        _objectid = objectid,
                        _ten = obj.ten,
                        _loaiho = obj.loaiho,
                        _vitri = obj.vitri,
                        _kinhdo = nulltoado,
                        _vido = nulltoado,
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _ngay = ngay,
                        _h = h,
                        _w = w,
                        _qvh = qvh,
                        _qxa = qxa,
                        _qcsi = qcsi,
                        _qcsii = qcsii,
                        _qcsiii = qcsiii,
                        _qtb = qtb,
                        _bh = bh,
                        _r = r,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _day = Convert.ToInt32(date[2]),
                        _month = Convert.ToInt32(date[1]),
                        _year = Convert.ToInt32(date[0])
                    }, commandType: CommandType.StoredProcedure
                );
            }
        }
        if (obj.ngay == null){
            int? nullDMY = null;
            DateTime? ngaynull = null;
            if (obj.kinhdo != null && obj.vido != null){
                return connection.ExecuteScalar<int>("EditHoChua", 
                    new{
                        _objectid = objectid,
                        _ten = obj.ten,
                        _loaiho = obj.loaiho,
                        _vitri = obj.vitri,
                        _kinhdo = Convert.ToDouble(Math.Round(Convert.ToDecimal(kinhdo), 6)),
                        _vido = Convert.ToDouble(Math.Round(Convert.ToDecimal(vido), 6)),
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _ngay = ngaynull,
                        _h = h,
                        _w = w,
                        _qvh = qvh,
                        _qxa = qxa,
                        _qcsi = qcsi,
                        _qcsii = qcsii,
                        _qcsiii = qcsiii,
                        _qtb = qtb,
                        _bh = bh,
                        _r = r,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _day = nullDMY,
                        _month = nullDMY,
                        _year = nullDMY
                    }, commandType: CommandType.StoredProcedure
                );
            }
            if (obj.kinhdo == null && obj.vido == null){
                return connection.ExecuteScalar<int>("EditHoChua", 
                    new{
                        _objectid = objectid,
                        _ten = obj.ten,
                        _loaiho = obj.loaiho,
                        _vitri = obj.vitri,
                        _kinhdo = nulltoado,
                        _vido = nulltoado,
                        _maxa = obj.maxa,
                        _mahuyen = obj.mahuyen,
                        _ngay = ngaynull,
                        _h = h,
                        _w = w,
                        _qvh = qvh,
                        _qxa = qxa,
                        _qcsi = qcsi,
                        _qcsii = qcsii,
                        _qcsiii = qcsiii,
                        _qtb = qtb,
                        _bh = bh,
                        _r = r,
                        _namcapnhat = namcapnhat,
                        _ghichu = nullstring,
                        _day = nullDMY,
                        _month = nullDMY,
                        _year = nullDMY
                    }, commandType: CommandType.StoredProcedure
                );
            }
        }
        return 1;
    }
    public int Delete(int objectid){
        return connection.ExecuteScalar<int>("DeleteHoChua", new{
            _objectid = objectid
        }, commandType: CommandType.StoredProcedure);
    }
}