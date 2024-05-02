--- thong ke bao
SELECT a.capdobao, a.tansuatxuathien, a.tencacconbao, ROUND((a.tansuatxuathien::numeric * 100 / b.tong), 2) AS phantramcapdobao, a.mamau
FROM 
(
	SELECT a.capbao AS capdobao, COUNT(*) AS tansuatxuathien, STRING_AGG(CONCAT(a.tenbao, ' (', a.year, ')'), '; ' ORDER BY a.year ASC) AS tencacconbao, c.color AS mamau
	FROM
	( 
		SELECT a.tenbao, a.capbao, a.year FROM Bao a WHERE a.year between 2015 and 2020 
		GROUP BY a.tenbao, a.capbao, a.year
	) a 
	LEFT JOIN Color c ON c.name = a.capbao
	GROUP BY a.capbao, c.color
) a, 
(	
		SELECT COUNT(a.tenbao) AS Tong 
		FROM 
		(
			SELECT a.tenbao, a.capbao, a.year 
			FROM Bao a WHERE a.year between 2015 and 2020 
			GROUP BY a.tenbao, a.capbao, a.year
		) a
) b

-- Thong ke do man chi tiet
SELECT x.year AS nam, z.tentram AS Tencactramdoman, z.Tongsophantu, x.doman AS domanthapnhat, x.ngay AS thoidiemdomanthapnhat, y.doman AS domancaonhat, y.ngay AS thoidiemdomancaonhat, z.domantb AS domantrungbinh 
FROM 
(
	SELECT a.year, a.min AS doman, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay  
	FROM 
	(
		SELECT a.year, MIN(a.doman) 
		FROM DoMan a WHERE a.ngay between '2015-02-28' and '2016-03-01' 
		AND a.doman != 0
		GROUP BY a.year	
	) a
	LEFT JOIN DoMan b ON b.year = a.year AND a.min = b.doman
	GROUP BY a.year, a.min
) x, 
(
	SELECT a.year, a.max AS doman, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay  
	FROM 
	(
		SELECT a.year, MAX(a.doman) 
		FROM DoMan a WHERE a.ngay between '2015-02-28' and '2016-03-01' 
		AND a.doman != 0
		GROUP BY a.year	
	) a
	LEFT JOIN DoMan b ON b.year = a.year AND a.max = b.doman
	GROUP BY a.year, a.max
) y, 	
(
	SELECT a.year, STRING_AGG( DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.doman)::Numeric, 2) AS domantb 
	FROM DoMan a WHERE a.ngay between '2015-02-28' and '2016-03-01' 
	AND a.doman != 0 
	GROUP BY a.year
) z
WHERE x.year = y.year AND y.year = z.year
ORDER BY x.year ASC


-- Thong ke do man toan bo
SELECT z.tentram AS Tencactramdoman, z.Tongsophantu, x.doman AS domanthapnhat, x.ngay AS thoidiemdomanthapnhat, y.doman AS domancaonhat, y.ngay AS thoidiemdomancaonhat, z.domantb AS domantrungbinh 
FROM 
(
	SELECT a.doman, STRING_AGG(a.ngay, '; ') AS Ngay 
	FROM 
	(
		SELECT a.doman, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM DoMan a 
		WHERE 
		a.ngay between '2015-02-28' and '2015-03-01'
		AND a.doman IN(
			SELECT MIN(a.doman) from (SELECT * FROM DoMan a WHERE a.ngay between '2015-02-28' and '2015-03-01' AND a.doman != 0) a
		)
		GROUP BY a.ngay, a.doman
		ORDER BY a.ngay ASC
	) a
	GROUP BY a.doman
) x, 
(
	SELECT a.doman, STRING_AGG(a.ngay, '; ') AS Ngay 
	FROM 
	(
		SELECT a.doman, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM DoMan a 
		WHERE 
		a.ngay between '2015-02-28' and '2015-03-01'
		AND a.doman IN(
			SELECT MAX(a.doman) from (SELECT * FROM DoMan a WHERE a.ngay between '2015-02-28' and '2015-03-01' AND a.doman != 0) a
		)
		GROUP BY a.ngay, a.doman
		ORDER BY a.ngay ASC
	) a
	GROUP BY a.doman
) y, 	
(
	SELECT STRING_AGG( DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.doman)::Numeric, 2) AS domantb 
	FROM (SELECT * FROM DoMan a WHERE a.ngay between '2015-02-28' and '2015-03-01' AND a.doman != 0) a  
) z

-- thong ke mua chi tiet tung nam
SELECT x.year AS nam, z.tentram AS Tentramdomua, z.Tongsophantu, x.min AS luongmuathapnhat, x.ngay AS thoidiemluongmuathapnhat, y.max AS luongmuacaonhat, y.ngay AS thoidiemluongmuacaonhat, z.luongmuatb AS luongmuatrungbinh 
FROM 
(
	SELECT a.year, a.min, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS ngay
	FROM
	(
		SELECT a.year, MIN(a.luongmua) 
		FROM Mua a 
		WHERE a.ngay between '2000-01-01' and '2003-01-30' 
		AND a.luongmua != 0 
		GROUP BY a.year
	) a
	LEFT JOIN Mua b ON b.year = a.year AND b.luongmua = a.min
	GROUP BY a.year, a.min
	ORDER BY a.year ASC
) x, 
(
	SELECT a.year, a.max, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS ngay
	FROM
	(
		SELECT a.year, MAX(a.luongmua) 
		FROM Mua a 
		WHERE a.ngay between '2000-01-01' and '2003-01-30' 
		AND a.luongmua != 0 
		GROUP BY a.year
	) a
	LEFT JOIN Mua b ON b.year = a.year AND b.luongmua = a.max
	GROUP BY a.year, a.max
	ORDER BY a.year ASC
) y, 	
(
	SELECT a.year, STRING_AGG(DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.luongmua)::Numeric, 2) AS luongmuatb 
	FROM Mua a 
	WHERE a.ngay between '2000-01-01' and '2003-01-30'
	AND a.luongmua != 0 
	GROUP BY a.year
) z
WHERE x.year = y.year AND y.year = z.year

-- thong ke mua toan bo
SELECT z.tentram AS Tentramdomua, z.Tongsophantu, x.luongmua AS luongmuathapnhat, x.ngay AS thoidiemluongmuathapnhat, y.luongmua AS luongmuacaonhat, y.ngay AS thoidiemluongmuacaonhat, z.luongmuatb AS luongmuatrungbinh 
FROM 
(
	SELECT a.luongmua, STRING_AGG(a.ngay, '; ') AS Ngay 
	FROM 
	(
		SELECT a.luongmua, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM Mua a 
		WHERE 
		a.ngay between '2000-01-31' and '2000-02-15'
		AND a.luongmua IN(
			SELECT MIN(a.luongmua) from (SELECT * FROM Mua a WHERE a.ngay between '2000-01-31' and '2000-02-15' AND a.luongmua != 0) a
		)
		GROUP BY a.ngay, a.luongmua
		ORDER BY a.ngay ASC
	) a
	GROUP BY a.luongmua
) x, 
(
	SELECT a.luongmua, STRING_AGG(a.ngay, '; ') AS Ngay 
	FROM 
	(
		SELECT a.luongmua, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM Mua a 
		WHERE 
		a.ngay between '2000-01-31' and '2000-02-15'
		AND a.luongmua IN(
			SELECT MAX(a.luongmua) from (SELECT * FROM Mua a WHERE a.ngay between '2000-01-31' and '2000-02-15' AND a.luongmua != 0) a
		)
		GROUP BY a.ngay, a.luongmua
		ORDER BY a.ngay ASC
	) a
	GROUP BY a.luongmua
) y, 	
(
	SELECT STRING_AGG( DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.luongmua)::Numeric, 2) AS luongmuatb 
	FROM (SELECT * FROM Mua a WHERE a.ngay between '2000-01-31' and '2000-02-15' AND a.luongmua != 0) a  
) z

-- thong ke muc nuoc chi tiet
SELECT x.year AS nam, z.tentram AS Tentramdomucnuoc, z.Tongsophantu, x.docaochantrieu AS mucnuocthapnhat, x.ngay AS thoidiemmucnuocthapnhat, y.docaodinhtrieu AS mucnuoccaonhat, y.ngay AS thoidiemmucnuoccaonhat, z.mucnuoctb AS mucnuoctrungbinh 
FROM 
(
	SELECT a.year, a.min AS docaochantrieu, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay 
	FROM 
	(
		SELECT a.year, MIN(a.docaochantrieu) 
		FROM MucNuoc a 
		WHERE a.ngay between '2008-02-01' and '2010-12-31' 
		AND a.docaodinhtrieu != 0
		GROUP BY a.year
	) a
	LEFT JOIN MucNuoc b ON b.year = a.year AND a.min = b.docaochantrieu
	GROUP BY a.year, a.min
) x, 
(
	SELECT a.year, a.max AS docaodinhtrieu, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay 
	FROM 
	(
		SELECT a.year, MAX(a.docaodinhtrieu) 
		FROM MucNuoc a 
		WHERE a.ngay between '2008-02-01' and '2010-12-31' 
		AND a.docaodinhtrieu != 0
		GROUP BY a.year
	) a
	LEFT JOIN MucNuoc b ON b.year = a.year AND a.max = b.docaodinhtrieu
	GROUP BY a.year, a.max
) y, 	
(
	SELECT a.year, STRING_AGG(DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG((((docaodinhtrieu)+(docaochantrieu))/2))::Numeric, 2) AS mucnuoctb 
	FROM MucNuoc a 
	WHERE a.ngay between '2008-02-01' and '2010-02-15' 
	AND a.docaodinhtrieu != 0 
	AND a.docaochantrieu != 0
	GROUP BY a.year
) z
WHERE x.year = y.year AND y.year = z.year
ORDER BY x.year ASC

-- thong ke mucnuoc toan bo
SELECT z.tentram AS Tentramdomucnuoc, z.Tongsophantu, x.docaochantrieu AS mucnuocthapnhat, x.ngay AS thoidiemmucnuocthapnhat, y.docaodinhtrieu AS mucnuoccaonhat, y.ngay AS thoidiemmucnuoccaonhat, z.mucnuoctb AS mucnuoctrungbinh 
FROM 
(
	SELECT a.docaochantrieu, STRING_AGG(a.ngay, '; ') AS Ngay 
	FROM 
	(
		SELECT a.docaochantrieu, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM MucNuoc a 
		WHERE 
		a.ngay between '2000-01-31' and '2015-02-15'
		AND a.docaochantrieu IN(
			SELECT MIN(a.docaochantrieu) from (SELECT * FROM MucNuoc a WHERE a.ngay between '2000-01-31' and '2015-02-15' AND a.docaochantrieu != 0) a
		)
		GROUP BY a.ngay, a.docaochantrieu
		ORDER BY a.ngay ASC
	) a
	GROUP BY a.docaochantrieu
) x, 
(
	SELECT a.docaodinhtrieu, STRING_AGG(a.ngay, '; ') AS Ngay 
	FROM 
	(
		SELECT a.docaodinhtrieu, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS Ngay FROM MucNuoc a 
		WHERE 
		a.ngay between '2008-02-01' and '2015-02-15'
		AND a.docaodinhtrieu IN(
			SELECT MAX(a.docaodinhtrieu) from (SELECT * FROM MucNuoc a WHERE a.ngay between '2008-02-01' and '2015-02-15' AND a.docaodinhtrieu != 0) a
		)
		GROUP BY a.ngay, a.docaodinhtrieu
		ORDER BY a.ngay ASC
	) a
	GROUP BY a.docaodinhtrieu
) y, 	
(
	SELECT STRING_AGG(DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG((((docaodinhtrieu)+(docaochantrieu))/2))::Numeric, 2) AS mucnuoctb 
	FROM (SELECT * FROM MucNuoc a WHERE a.ngay between '2008-02-01' and '2015-02-15' AND a.docaodinhtrieu != 0 AND a.docaochantrieu != 0) a 
) z

-- thong ke ho chua chi tiet
SELECT a.nam, a.tencachochua, a.tongsophantu, a.thongso, a.luuluongthapnhat, a.thoidiemluuluongthapnhat, a.luuluongcaonhat, a.thoidiemluuluongcaonhat, luuluongtrungbinh
FROM
(
(SELECT x.year AS nam, z.ten AS tencachochua, z.Tongsophantu, 'Mực nước'::Text AS Thongso, x.h AS luuluongthapnhat, x.ngay AS thoidiemluuluongthapnhat, y.h AS luuluongcaonhat, y.ngay AS thoidiemluuluongcaonhat, z.mucnuoctrungbinh AS luuluongtrungbinh 
FROM 
(
	SELECT a.year, a.min AS h, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay 
	FROM  
	(
		SELECT a.year, MIN(a.h) 
		from HoChua a 
		WHERE a.ngay between '2008-02-01' and '2009-02-15' 
		AND a.h != 0
		GROUP BY a.year
	) a
	LEFT JOIN HoChua b ON b.year = a.year AND a.min = b.h
	GROUP BY a.year, a.min
) x,
(
	SELECT a.year, a.max AS h, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay 
	FROM  
	(
		SELECT a.year, MAX(a.h) 
		from HoChua a 
		WHERE a.ngay between '2008-02-01' and '2009-02-15' 
		AND a.h != 0
		GROUP BY a.year
	) a
	LEFT JOIN HoChua b ON b.year = a.year AND a.max = b.h
	GROUP BY a.year, a.max
) y, 
(
	SELECT a.year, STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.h)::Numeric, 2) AS mucnuoctrungbinh	
	FROM HoChua a 
	WHERE a.ngay between '2008-02-01' and '2009-02-15' 
	AND a.h != 0 
	GROUP BY a.year
) z 
WHERE x.year = y.year AND y.year = z.year
ORDER BY x.year ASC)

UNION ALL 

(SELECT x.year AS nam, z.ten AS tencachochua, z.Tongsophantu, 'Lưu lượng nước về hồ'::Text AS Thongso, x.min AS luuluongthapnhat, x.ngay AS thoidiemluuluongthapnhat, y.max AS luuluongcaonhat, y.ngay AS thoidiemluuluongcaonhat, z.mucnuoctrungbinh AS luuluongtrungbinh 
FROM 
(
	SELECT a.year, a.min, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay 
	FROM  
	(
		SELECT a.year, MIN(a.qvh) 
		from HoChua a 
		WHERE a.ngay between '2008-02-01' and '2009-02-15' 
		AND a.qvh != 0
		GROUP BY a.year
	) a
	LEFT JOIN HoChua b ON b.year = a.year AND a.min = b.qvh
	GROUP BY a.year, a.min
) x,
(
	SELECT a.year, a.max, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay 
	FROM  
	(
		SELECT a.year, MAX(a.qvh) 
		from HoChua a 
		WHERE a.ngay between '2008-02-01' and '2009-02-15' 
		AND a.qvh != 0
		GROUP BY a.year
	) a
	LEFT JOIN HoChua b ON b.year = a.year AND a.max = b.qvh
	GROUP BY a.year, a.max
) y, 
(
	SELECT a.year, STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.qvh)::Numeric, 2) AS mucnuoctrungbinh	
	FROM HoChua a 
	WHERE a.ngay between '2008-02-01' and '2009-02-15' 
	AND a.qvh != 0 
	GROUP BY a.year
) z 
WHERE x.year = y.year AND y.year = z.year
ORDER BY x.year ASC)

UNION ALL 

(SELECT x.year AS nam, z.ten AS tencachochua, z.Tongsophantu, 'Lưu lượng nước xã tràn'::Text AS Thongso, x.min AS luuluongthapnhat, x.ngay AS thoidiemluuluongthapnhat, y.max AS luuluongcaonhat, y.ngay AS thoidiemluuluongcaonhat, z.mucnuoctrungbinh AS luuluongtrungbinh 
FROM 
(
	SELECT a.year, a.min, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay 
	FROM  
	(
		SELECT a.year, MIN(a.qxa) 
		from HoChua a 
		WHERE a.ngay between '2008-02-01' and '2009-02-15' 
		AND a.qxa != 0
		GROUP BY a.year
	) a
	LEFT JOIN HoChua b ON b.year = a.year AND a.min = b.qxa
	GROUP BY a.year, a.min
) x,
(
	SELECT a.year, a.max, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay 
	FROM  
	(
		SELECT a.year, MAX(a.qxa) 
		from HoChua a 
		WHERE a.ngay between '2008-02-01' and '2009-02-15' 
		AND a.qxa != 0
		GROUP BY a.year
	) a
	LEFT JOIN HoChua b ON b.year = a.year AND a.max = b.qxa
	GROUP BY a.year, a.max
) y, 
(
	SELECT a.year, STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.qxa)::Numeric, 2) AS mucnuoctrungbinh	
	FROM HoChua a 
	WHERE a.ngay between '2008-02-01' and '2009-02-15' 
	AND a.qxa != 0 
	GROUP BY a.year
) z 
WHERE x.year = y.year AND y.year = z.year
ORDER BY x.year ASC)
) a
ORDER BY a.nam ASC, a.tencachochua ASC

--- thong ke ho chua toan bo
SELECT z.ten AS tencachochua, z.Tongsophantu, 'Mực nước'::Text AS Thongso, x.h AS luuluongthapnhat, x.ngay AS thoidiemluuluongthapnhat, y.h AS luuluongcaonhat, y.ngay AS thoidiemluuluongcaonhat, z.mucnuoctrungbinh AS luuluongtrungbinh 
FROM 
(
	SELECT a.h, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay 
	FROM HoChua a 
	WHERE a.ngay between '2008-02-01' and '2008-02-15' AND a.h IN
	(
		SELECT MIN(a.h) from HoChua a WHERE a.ngay between '2008-02-01' and '2008-02-15' AND a.h != 0
	) 
	GROUP BY a.h
) x,
(
	SELECT a.h, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay 
	FROM HoChua a 
	WHERE a.ngay between '2008-02-01' and '2008-02-15' AND a.h IN 
	(
		SELECT MAX(a.h) from HoChua a WHERE a.ngay between '2008-02-01' and '2008-02-15' AND a.h != 0
	) 
	GROUP BY a.h
) y, 
(
	SELECT STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.h)::Numeric, 2) AS mucnuoctrungbinh	
	FROM HoChua a WHERE a.ngay between '2008-02-01' and '2008-02-15' AND a.h != 0 
) z 
UNION ALL 
SELECT z.ten AS Ten, z.Tongsophantu, 'Lưu lượng nước về hồ'::Text AS Thongso, x.qvh AS thapnhat, x.ngay AS Thoidiemthapnhat, y.qvh AS caonhat, y.ngay AS Thoidiemcaonhat, z.vehotrungbinh AS luuluongtrungbinh 
FROM 
(
	SELECT a.qvh, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay 
	FROM HoChua a WHERE a.ngay between '2008-02-01' and '2008-02-15' AND a.qvh IN
	(
		SELECT MIN(a.qvh) from HoChua a WHERE a.ngay between '2008-02-01' and '2008-02-15' AND a.qvh != 0
	)
	GROUP BY a.qvh
) x,
(
	SELECT a.qvh, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay 
	FROM HoChua a WHERE a.ngay between '2008-02-01' and '2008-02-15' AND a.qvh IN 
	(
		SELECT MAX(a.qvh) from HoChua a WHERE a.ngay between '2008-02-01' and '2008-02-15' AND a.qvh != 0
	) 
	GROUP BY a.qvh
) y, 
(
	SELECT STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.qvh)::Numeric, 2) AS vehotrungbinh	
	FROM HoChua a WHERE a.ngay between '2008-02-01' and '2008-02-15' AND a.qvh != 0 
) z 
UNION ALL 
SELECT z.ten AS Ten, z.Tongsophantu, 'Lưu lượng nước xã tràn'::Text AS Thongso, x.qxa AS thapnhat, x.ngay AS Thoidiemthapnhat, y.qxa AS caonhat, y.ngay AS Thoidiemcaonhat, z.xatrungbinh AS luuluongtrungbinh 
FROM 
(
	SELECT a.qxa, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay 
	FROM HoChua a WHERE  a.ngay between '2008-02-01' and '2008-02-15' AND a.qxa IN
	(
		SELECT MIN(a.qxa) from HoChua a WHERE a.ngay between '2008-02-01' and '2008-02-15' AND a.qxa != 0
	) 
	GROUP BY a.qxa
) x,
(
	SELECT a.qxa, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay 
	FROM HoChua a WHERE a.ngay between '2008-02-01' and '2008-02-15' AND a.qxa IN 
	(
		SELECT MAX(a.qxa) from HoChua a WHERE a.ngay between '2008-02-01' and '2008-02-15' AND a.qxa != 0
	) 
	GROUP BY a.qxa
) y, 
(
	SELECT STRING_AGG(DISTINCT a.ten, '; ') AS ten, COUNT(a.ten) AS Tongsophantu, ROUND(AVG(a.qxa)::Numeric, 2) AS xatrungbinh	
	FROM HoChua a WHERE a.ngay between '2008-02-01' and '2008-02-15' AND a.qxa != 0 
) z

-- thong ke nang nong chi tiet
SELECT x.year AS nam, z.tentram AS Tentramdonhietdo, z.Tongsophantu, x.min AS nhietdothapnhat, x.ngay AS thoidiemnhietdothapnhat, y.max AS nhietdocaonhat, y.ngay AS thoidiemnhietdocaonhat, z.nhietdotb AS nhietdotrungbinh 
FROM 
(
	SELECT a.year, a.min, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay 
	FROM
	(
		SELECT a.year, MIN(a.nhietdomin) 
		FROM NangNong a 
		WHERE a.ngay between '2009-01-01' AND '2010-02-02' 
		AND a.nhietdomin != 0
		GROUP BY a.year
	) a	
	LEFT JOIN NangNong b ON b.year = a.year AND a.min = b.nhietdomin
	GROUP BY a.year, a.min
) x, 
(
	SELECT a.year, a.max, STRING_AGG(TO_CHAR(b.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY b.ngay ASC) AS Ngay 
	FROM
	(
		SELECT a.year, MAX(a.nhietdomax) 
		FROM NangNong a 
		WHERE a.ngay between '2009-01-01' AND '2010-02-02' 
		AND a.nhietdomax != 0
		GROUP BY a.year
	) a	
	LEFT JOIN NangNong b ON b.year = a.year AND a.max = b.nhietdomax
	GROUP BY a.year, a.max
) y, 
(
	SELECT a.year, STRING_AGG(DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.nhietdotb)::Numeric, 2) AS nhietdotb	
	FROM NangNong a 
	WHERE a.ngay between '2009-01-01' AND '2010-02-02' 
	AND a.nhietdotb != 0 
	GROUP BY a.year
) z
WHERE x.year = y.year AND y.year = z.year
ORDER BY x.year ASC

-- thong ke nang nong toan bo
SELECT z.tentram AS Tentramdonhietdo, z.Tongsophantu, x.nhietdomin AS nhietdothapnhat, x.ngay AS thoidiemnhietdothapnhat, y.nhietdomax AS nhietdocaonhat, y.ngay AS thoidiemnhietdocaonhat, z.nhietdotb AS nhietdotrungbinh 
FROM 
(
	SELECT a.nhietdomin, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay 
	FROM NangNong a WHERE a.ngay between '2000-01-01' AND '2020-02-02' AND a.nhietdomin IN
	(
		SELECT MIN(a.nhietdomin) from NangNong a WHERE a.ngay between '2000-01-01' AND '2020-02-02' AND a.nhietdomin != 0
	) 
	GROUP BY a.nhietdomin
) x, 
(
	SELECT a.nhietdomax, STRING_AGG(TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text, '; ' ORDER BY a.ngay) AS Ngay 
	FROM NangNong a WHERE a.ngay between '2000-01-01' AND '2020-02-02'	AND a.nhietdomax IN 
	(
		SELECT MAX(a.nhietdomax) from NangNong a WHERE a.ngay between '2000-01-01' AND '2020-02-02' AND a.nhietdomax != 0
	)	
	GROUP BY a.nhietdomax
) y, 
(
	SELECT STRING_AGG(DISTINCT a.tentram, '; ') AS tentram, COUNT(a.tentram) AS Tongsophantu, ROUND(AVG(a.nhietdotb)::Numeric, 2) AS nhietdotb	
	FROM NangNong a WHERE a.ngay between '2000-01-01' AND '2020-02-02' AND a.nhietdotb != 0 
) z

-- search bao
SELECT a.objectid, a.idbao, a.tenbao, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, a.toadox, a.toadoy, a.apsuat, a.tocdogio, a.vitri, a.maxa, CONCAT(a.mahuyen, ' - ', h.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 3) AS kinhdo, ROUND(a.vido::Numeric, 3) AS vido, a.capbao, a.ngaybatdau, a.ngayketthuc, a.centerid, a.tenvn, a.kvahhcm, a.shape , x.line 
FROM Bao a 
LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
LEFT JOIN 
(
	SELECT a.tenbao, a.year, ST_MakeLine(a.shape order by a.ngay asc, a.gio asc) AS line 
	FROM Bao a 
	LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
	WHERE a.tenbao = 'MARIAN' 
	GROUP BY a.year, a.tenbao
) x ON x.tenbao = a.tenbao AND x.year = a.year 
WHERE a.tenbao = 'MARIAN' 
ORDER BY a.ngay ASC, a.gio ASC

-- search apthapnhietdoi
SELECT a.objectid, a.idapthap, a.tenapthap, a.gio, TO_CHAR(a.ngay, 'dd/mm/yyyy')::Text AS ngay, ROUND(a.toadox::Numeric, 3) AS toadox, ROUND(a.toadoy::Numeric, 3) AS toadoy, a.apsuat, a.tocdogio, a.vitri, CONCAT(a.maxa, ' - ', xa.tenxa)::character varying AS maxa, CONCAT(a.mahuyen, ' - ', xa.tenhuyen)::character varying AS mahuyen, a.namcapnhat, a.ghichu, ROUND(a.kinhdo::Numeric, 3) AS kinhdo, ROUND(a.vido::Numeric, 3) AS vido, TO_CHAR(a.ngaybatdau, 'dd/mm/yyyy')::Text AS ngaybatdau, TO_CHAR(a.ngayketthuc, 'dd/mm/yyyy')::Text AS ngayketthuc, a.centerid, a.tenvn, a.kvahhcm, a.shape, x.line 
FROM ApThapNhietDoi a 
LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen 
LEFT JOIN 
(
	SELECT a.tenapthap, a.year, ST_MakeLine(a.shape order by a.ngay asc, a.gio asc) AS line 
	FROM ApThapNhietDoi a
	LEFT JOIN RgHuyen h ON h.MaHuyen = a.MaHuyen
	GROUP BY a.year, a.tenapthap
) x ON x.tenapthap = a.tenapthap AND x.year = a.year 
ORDER BY a.ngay ASC, a.gio ASC

-- thong ke ap thap nhiet doi
SELECT a.nam::Text, a.tongsoATND, a.tenATND  
FROM 
(
	SELECT a.year AS nam, COUNT(a.year) AS tongsoATND, STRING_AGG(a.tenapthap, '; ') AS tenATND FROM 
	(
		SELECT a.tenapthap, a.year FROM ApThapNhietDoi a 
		where a.year between 2010 and 2020
		GROUP BY a.tenapthap, a.year
		ORDER BY a.year ASC, a.tenapthap ASC
	) a 
	GROUP BY a.year
) a
UNION ALL
SELECT 'Tổng cộng' AS nam, COUNT(*) AS tongsoatnd, STRING_AGG(a.tenapthap, '; ') AS tenATND 
FROM
(
	SELECT a.tenapthap, a.year FROM ApThapNhietDoi a 
	WHERE a.year between 2010 and 2020
	GROUP BY a.tenapthap, a.year
) a

-- thong ke loc xoay 
SELECT a.tenhuyen AS quan_huyen_tp, a.tongsoloc
FROM
(
	SELECT h.tenhuyen, COUNT(h.tenhuyen) tongsoloc
	FROM LocXoay a
	JOIN RgHuyen h ON a.mahuyen = h.mahuyen
	WHERE a.year BETWEEN 2019 AND 2020
	GROUP BY h.tenhuyen
	ORDER BY h.tenhuyen ASC
) a
UNION ALL
SELECT 'Tổng cộng' AS quan_huyen_tp, COUNT(*) AS tongsoloc
FROM LocXoay a
WHERE a.year BETWEEN 2019 AND 2020

-- thong ke tuyen sat lo chi tiet
SELECT h.tenhuyen AS quan_huyen_tp, a.mucdo AS mucdosatlo, COUNT(a.mucdo) AS Soluongvitrisatlo, ROUND(SUM(a.chieudai)::Numeric, 2) AS Tongchieudaisatlo, c.color AS mamau
FROM SatLo_Line a
LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen
LEFT JOIN Color c ON a.mucdo = c.name
GROUP BY h.tenhuyen, a.mucdo, c.color
ORDER BY h.tenhuyen ASC,
CASE 
	WHEN a.mucdo = 'Sạt lở bình thường' THEN 1
	WHEN a.mucdo = 'Sạt lở nguy hiểm' THEN 2
	ELSE 3 END
ASC
	
-- thong ke tuyen sat lo thanh pho	
(SELECT  a.mucdo AS mucdosatlo, COUNT(a.mucdo) AS Soluongvitrisatlo, ROUND(SUM(a.chieudai)::Numeric, 2) AS Tongchieudaisatlo, c.color AS mamau, 'Mức độ sạt lở' AS phamvithongke
FROM SatLo_Line a
LEFT JOIN Color c ON a.mucdo = c.name
GROUP BY  a.mucdo, c.color
ORDER BY CASE 
	WHEN a.mucdo = 'Sạt lở bình thường' THEN 1
	WHEN a.mucdo = 'Sạt lở nguy hiểm' THEN 2
	ELSE 3 END
ASC)

-- thong ke sat lo point chi tiet
SELECT h.tenhuyen AS quan_huyen_tp, a.mucdo AS mucdosatlo, COUNT(a.mucdo) AS Soluongvitrisatlo, ROUND(SUM(a.chieudai)::Numeric, 2) AS Tongchieudaisatlo, c.color AS mamau
FROM SatLo_Point a
LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen
LEFT JOIN Color c ON a.mucdo = c.name
GROUP BY h.tenhuyen, a.mucdo, c.color
ORDER BY h.tenhuyen ASC,
CASE 
	WHEN a.mucdo = 'Sạt lở bình thường' THEN 1
	WHEN a.mucdo = 'Sạt lở nguy hiểm' THEN 2
	ELSE 3 END
ASC
	
-- thong ke sat lo point toan thanh pho	
(SELECT  a.mucdo AS mucdosatlo, COUNT(a.mucdo) AS Soluongvitrisatlo, ROUND(SUM(a.chieudai)::Numeric, 2) AS Tongchieudaisatlo, c.color AS mamau, 'Mức độ sạt lở' AS phamvithongke
FROM SatLo_Point a
LEFT JOIN Color c ON a.mucdo = c.name
GROUP BY  a.mucdo, c.color
ORDER BY CASE 
	WHEN a.mucdo = 'Sạt lở bình thường' THEN 1
	WHEN a.mucdo = 'Sạt lở nguy hiểm' THEN 2
	ELSE 3 END
ASC)


-- thong ke ke
SELECT a.quan_huyen_tp, a.tongsophantu, a.tongchieudaike
FROM
(
	(SELECT h.tenhuyen AS quan_huyen_tp, COUNT(h.tenhuyen) AS tongsophantu, ROUND(SUM(a.chieudai)::Numeric, 2) AS tongchieudaike 
	FROM Ke a
	LEFT JOIN RgHuyen AS h ON ST_Intersects(ST_Transform(h.shape, 4326), ST_Transform(a.shape, 4326)) 
	WHERE h.mahuyen = '787'
	GROUP BY h.tenhuyen)
	UNION ALL
	(SELECT 'Tổng cộng' AS quan_huyen_tp, COUNT(h.tenhuyen) AS tongsophantu, ROUND(SUM(a.chieudai)::Numeric, 2) AS tongchieudaike
	FROM Ke a
	LEFT JOIN RgHuyen AS h ON ST_Intersects(ST_Transform(h.shape, 4326), ST_Transform(a.shape, 4326))
	WHERE h.mahuyen = '787')
	
) a
ORDER BY 
CASE 
	WHEN a.quan_huyen_tp LIKE 'Huyện%' THEN 1
	WHEN a.quan_huyen_tp LIKE 'Quận%' THEN 2
	WHEN a.quan_huyen_tp LIKE 'Thành%' THEN 3
	ELSE 4 END
ASC
	
-- thong ke de bao, bo bao
SELECT a.donviquanly, a.tongsophantutheodvql, a.Tongchieudaidebaobobao
FROM
(
	(SELECT a.donviquanly, COUNT((CASE WHEN a.donviquanly IS NULL THEN '0' ELSE a.donviquanly END)) AS tongsophantutheodvql, ROUND(SUM(a.chieudai)::Numeric, 2) AS Tongchieudaidebaobobao 
	FROM Debao_BoBao a
	LEFT JOIN RgHuyen AS h ON ST_Intersects(ST_Transform(h.shape, 4326), ST_Transform(a.shape, 4326))
	WHERE h.MaHuyen = '787' 
	GROUP BY a.donviquanly 
	ORDER BY a.donviquanly ASC)
	UNION ALL
	(SELECT 'Tổng cộng' AS donviquanly, COUNT(*) AS tongsophantutheodvql, ROUND(SUM(a.chieudai)::Numeric, 2) AS Tongchieudaidebaobobao
	FROM Debao_BoBao a
	LEFT JOIN RgHuyen AS h ON ST_Intersects(ST_Transform(h.shape, 4326), ST_Transform(a.shape, 4326))
	WHERE h.mahuyen = '787'
	) 
) a
ORDER BY a.donviquanly isnull DESC, a.donviquanly = 'Tổng cộng' ASC

-- thong ke cong dap chi tiet
SELECT a.donviquanly, a.capcongtrinh, COUNT(CASE WHEN a.capcongtrinh IS NULL THEN '0' ELSE a.capcongtrinh END) AS tongsocapcongtrinh, ROUND(SUM(a.chieudai)::Numeric, 2) AS tongchieudaitheocapcongtrinh, (CASE WHEN c.color ISNULL THEN (SELECT color FROM color WHERE name = 'Đang cập nhật') ELSE c.color END) AS mamau
FROM CongDap a
LEFT JOIN Color c ON a.capcongtrinh = c.name
LEFT JOIN RgHuyen AS h ON ST_Intersects(ST_Transform(h.shape, 4326), ST_Transform(a.shape, 4326))
GROUP BY a.donviquanly, a.capcongtrinh, c.color
ORDER BY a.donviquanly ASC, a.capcongtrinh ASC

-- thong ke cong dap toan thanh pho
SELECT a.capcongtrinh, COUNT(CASE WHEN a.capcongtrinh IS NULL THEN '0' ELSE a.capcongtrinh END) AS tongsocapcongtrinh, ROUND(SUM(a.chieudai)::Numeric, 2) AS tongchieudaitheocapcongtrinh, (CASE WHEN c.color ISNULL THEN (SELECT color FROM color WHERE name = 'Đang cập nhật') ELSE c.color END) AS mamau, 'Cấp công trình' AS phamvithongke 
FROM CongDap a
LEFT JOIN Color c ON a.capcongtrinh = c.name
GROUP BY a.capcongtrinh, c.color
ORDER BY a.capcongtrinh ASC

-- thong ke moc canh bao trieu cuong
(SELECT h.tenhuyen AS quan_huyen_tp, COUNT(h.tenhuyen) AS Tongsomoccanhbaotrieucuong 
FROM MocCanhBaoTrieuCuong a
LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen
GROUP BY h.tenhuyen
ORDER BY h.tenhuyen ASC)
UNION ALL
(SELECT 'Tổng cộng' AS quan_huyen_tp, COUNT(h.tenhuyen) AS Tongsomoccanhbaotrieucuong
FROM MocCanhBaoTrieuCuong a
LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen)

-- thong ke bien canh bao sat lo
(SELECT h.tenhuyen AS quan_huyen_tp, COUNT(a.vitrisatlo) AS Tongsobiencanhbaosatlo, STRING_AGG(DISTINCT a.tuyensr, '; ' ORDER BY a.tuyensr ASC) AS tentuyensr
FROM BienCanhBaoSatLo a
LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen
GROUP BY h.tenhuyen
ORDER BY h.tenhuyen ASC)
UNION ALL
(SELECT 'Tổng cộng' AS quan_huyen_tp, COUNT(a.vitrisatlo) AS Tongsobiencanhbaosatlo, STRING_AGG(DISTINCT a.tuyensr, '; ' ORDER BY a.tuyensr ASC) AS tentuyensr 
FROM BienCanhBaoSatLo a
LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen)

-- thong ke khu neo dau tau thuyen
(SELECT h.tenhuyen AS quan_huyen_tp, COUNT(h.tenhuyen) AS Tongsokhuneodautauthuyen 
FROM KhuNeoDau a
LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen
GROUP BY h.tenhuyen
ORDER BY h.tenhuyen ASC)
UNION ALL
(SELECT 'Tổng cộng' AS quan_huyen_tp, COUNT(h.tenhuyen) AS Tongsokhuneodautauthuyen 
FROM KhuNeoDau a
LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen)

-- thong ke vi tri xung yeu
(SELECT h.tenhuyen AS quan_huyen_tp, COUNT(h.tenhuyen) AS Tongsovitrixungyeu, STRING_AGG(DISTINCT a.phuongan, '; ' ORDER BY a.phuongan DESC) AS vitrixungyeutheophuongan 
FROM DiemXungYeu a
LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen
GROUP BY h.tenhuyen
ORDER BY h.tenhuyen ASC)
UNION ALL
(SELECT 'Tổng cộng' AS quan_huyen_tp, COUNT(h.tenhuyen) AS Tongsovitrixungyeu, STRING_AGG(DISTINCT a.phuongan, '; ' ORDER BY a.phuongan DESC) AS vitrixungyeutheophuongan 
FROM DiemXungYeu a
LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen)

-- thong ke vi tri an toan
(SELECT h.tenhuyen AS quan_huyen_tp, COUNT(h.tenhuyen) AS Tongsovitriantoan, STRING_AGG(DISTINCT a.phuongan, '; ' ORDER BY a.phuongan DESC) AS vitriantoantheophuongan 
FROM DiemAnToan a
LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen
GROUP BY h.tenhuyen
ORDER BY h.tenhuyen ASC)
UNION ALL
(SELECT 'Tổng cộng' AS quan_huyen_tp, COUNT(h.tenhuyen) AS Tongsovitriantoan, STRING_AGG(DISTINCT a.phuongan, '; ' ORDER BY a.phuongan DESC) AS vitriantoantheophuongan 
FROM DiemAnToan a
LEFT JOIN RgHuyen h ON a.mahuyen = h.mahuyen)


-- thong ke du kien di dan
(SELECT a.QuanHuyen AS quan_huyen_tp, SUM(a.soho_bao8_9) AS Tongsohodidoibao8_9, SUM(a.songuoi_bao8_9) AS Tongsonguoididoibao8_9,SUM(a.soho_bao10_13) AS tongsohodidoibao10_13, SUM(a.songuoi_bao10_13) AS tongsonguoididoibao10_13 
FROM DuKienDiDan a
GROUP BY a.QuanHuyen
ORDER BY a.QuanHuyen ASC)
UNION ALL
(SELECT 'Tổng cộng' AS quan_huyen_tp, SUM(a.soho_bao8_9) AS Tongsohodidoibao8_9, SUM(a.songuoi_bao8_9) AS Tongsonguoididoibao8_9,SUM(a.soho_bao10_13) AS tongsohodidoibao10_13, SUM(a.songuoi_bao10_13) AS tongsonguoididoibao10_13 
FROM DuKienDiDan a)

-- thong ke luc luong huy dong chi tiet cho toan thanh pho - su dung bang "lucluonghuydong"
SELECT t.qhtp, t.tenlucluong, SUM(t.thanhpho) AS Thanhpho, SUM(t.quanhuyen) AS quanhuyen, SUM(t.phuongxathitran) AS phuongxathitran, SUM(t.apkhupho) AS apkhupho, SUM((CASE WHEN t.thanhpho IS NULL THEN 0 ELSE t.thanhpho END) + (CASE WHEN t.quanhuyen IS NULL THEN 0 ELSE t.quanhuyen END) + (CASE WHEN t.phuongxathitran IS NULL THEN 0 ELSE t.phuongxathitran END) + (CASE WHEN t.apkhupho IS NULL THEN 0 ELSE t.apkhupho END)) AS Tongcong 
FROM
(
	SELECT qhtp, tenlucluong,
		(CASE WHEN a.capql = 'Thành phố' THEN SUM(a.slnguoihd) END) AS Thanhpho,
		(CASE WHEN a.capql = 'Quận, huyện' THEN SUM(a.slnguoihd) END) AS quanhuyen,
		(CASE WHEN a.capql = 'Phường, xã, thị trấn' THEN SUM(a.slnguoihd) END) AS phuongxathitran,
		(CASE WHEN a.capql = 'Ấp, khu phố' THEN SUM(a.slnguoihd) END) AS apkhupho
		FROM lucluonghuydong a
	GROUP BY a.qhtp, a.tenlucluong, a.capql
	ORDER BY a.qhtp ASC
) AS t
GROUP BY t.qhtp, t.tenlucluong
ORDER BY t.qhtp ASC, t.tenlucluong ASC

-- thong ke luc luong huy dong toan thanh pho - draf
(SELECT t.tenlucluong, SUM(t.thanhpho) AS Thanhpho, SUM(t.quanhuyen) AS quanhuyen, SUM(t.phuongxathitran) AS phuongxathitran, SUM(t.apkhupho) AS apkhupho, SUM((CASE WHEN t.thanhpho IS NULL THEN 0 ELSE t.thanhpho END) + (CASE WHEN t.quanhuyen IS NULL THEN 0 ELSE t.quanhuyen END) + (CASE WHEN t.phuongxathitran IS NULL THEN 0 ELSE t.phuongxathitran END) + (CASE WHEN t.apkhupho IS NULL THEN 0 ELSE t.apkhupho END)) AS Tongcong, 'Toàn thành phố' AS phamvithongke
FROM
(
	SELECT a.tenlucluong::text, 
		(CASE WHEN a.capql = 'Thành phố' THEN SUM(a.slnguoihd) END) AS Thanhpho,
		(CASE WHEN a.capql = 'Quận, huyện' THEN SUM(a.slnguoihd) END) AS quanhuyen,
		(CASE WHEN a.capql = 'Phường, xã, thị trấn' THEN SUM(a.slnguoihd) END) AS phuongxathitran,
		(CASE WHEN a.capql = 'Ấp, khu phố' THEN SUM(a.slnguoihd) END) AS apkhupho
	FROM lucluonghuydong a
	GROUP BY a.tenlucluong, a.capql
) AS t
group by t.tenlucluong
ORDER BY t.tenlucluong ASC)
UNION ALL
(SELECT t.tenlucluong, SUM(t.thanhpho) AS Thanhpho, SUM(t.quanhuyen) AS quanhuyen, SUM(t.phuongxathitran) AS phuongxathitran, SUM(t.apkhupho) AS apkhupho, SUM((CASE WHEN t.thanhpho IS NULL THEN 0 ELSE t.thanhpho END) + (CASE WHEN t.quanhuyen IS NULL THEN 0 ELSE t.quanhuyen END) + (CASE WHEN t.phuongxathitran IS NULL THEN 0 ELSE t.phuongxathitran END) + (CASE WHEN t.apkhupho IS NULL THEN 0 ELSE t.apkhupho END)) AS Tongcong, 'Toàn thành phố' AS phamvithongke
FROM
(
	SELECT 'Tổng các lực lượng' AS tenlucluong, 
		(CASE WHEN a.capql = 'Thành phố' THEN SUM(a.slnguoihd) END) AS Thanhpho,
		(CASE WHEN a.capql = 'Quận, huyện' THEN SUM(a.slnguoihd) END) AS quanhuyen,
		(CASE WHEN a.capql = 'Phường, xã, thị trấn' THEN SUM(a.slnguoihd) END) AS phuongxathitran,
		(CASE WHEN a.capql = 'Ấp, khu phố' THEN SUM(a.slnguoihd) END) AS apkhupho
	FROM lucluonghuydong a
	GROUP BY a.capql
) AS t
group by t.tenlucluong)

-- thong ke luc luong huy dong toan thanh pho
(SELECT a.tenlucluong::text, SUM(a.slnguoihd) AS tongcong, 'Lực lượng' AS phamvithongke
FROM lucluonghuydong a
GROUP BY a.tenlucluong
ORDER BY a.tenlucluong ASC)


-- thong ke phuong tien huy dong chi tiet
SELECT a.dvql AS donviquanly, a.tenphuongtienttb, SUM(a.soluong) AS Soluongphuongtienttb, a.dvt AS Donvitinh
FROM PhuongTienHuyDong a
GROUP BY a.dvql, a.tenphuongtienttb, a.dvt
ORDER BY CASE WHEN a.dvql LIKE 'Bộ%' THEN 1
			  WHEN a.dvql LIKE 'BCH%' THEN 2 
			  WHEN a.dvql LIKE 'Công an%' THEN 3
			  WHEN a.dvql LIKE 'Sở Tài nguyên%' THEN 4
			  WHEN a.dvql LIKE 'Sở Giáo dục%' THEN 5
			  WHEN a.dvql LIKE 'Sở Lao động%' THEN 6
			  WHEN a.dvql LIKE 'Lực lượng%' THEN 7
			  WHEN a.dvql LIKE 'Công ty%' THEN 8
			  WHEN a.dvql LIKE 'Công viên%' THEN 9
			  WHEN a.dvql LIKE 'Trung tâm%' THEN 10
			  WHEN a.dvql LIKE 'Cảng%' THEN 11
			  WHEN a.dvql LIKE 'Thành phố%' THEN 12
			  WHEN a.dvql = 'Quận 1' THEN 13
			  WHEN a.dvql = 'Quận 3' THEN 14
			  WHEN a.dvql = 'Quận 4' THEN 15
			  WHEN a.dvql = 'Quận 5' THEN 16
			  WHEN a.dvql = 'Quận 6' THEN 17
			  WHEN a.dvql = 'Quận 7' THEN 18
			  WHEN a.dvql = 'Quận 8' THEN 19
			  WHEN a.dvql = 'Quận 10' THEN 20
			  WHEN a.dvql = 'Quận 11' THEN 21
			  WHEN a.dvql = 'Quận 12' THEN 22
			  WHEN a.dvql = 'Quận Gò Vấp' THEN 23
			  WHEN a.dvql = 'Quận Phú Nhuận' THEN 24
			  WHEN a.dvql = 'Quận Tân Bình' THEN 25
			  WHEN a.dvql = 'Quận Tân Phú' THEN 26
			  WHEN a.dvql = 'Quận Bình Tân' THEN 27
			  WHEN a.dvql = 'Quận Bình Thạnh' THEN 28
			  WHEN a.dvql = 'Huyện Bình Chánh' THEN 29
			  WHEN a.dvql = 'Huyện Củ Chi' THEN 30
			  WHEN a.dvql = 'Huyện Hóc Môn' THEN 31
			  WHEN a.dvql = 'Huyện Nhà Bè' THEN 32
			  WHEN a.dvql = 'Huyện Cần Giờ' THEN 33
		 	  ElSE 34 END
ASC
			  
-- thong ke phuong tien huy dong toan thanh pho
SELECT a.tenphuongtienttb, a.dvt AS Donvitinh, SUM(a.soluong) AS Soluongphuongtienttb, STRING_AGG(CONCAT(a.dvql, ' (', a.soluong, ')'), '; ' ORDER BY 
	CASE 
		WHEN a.dvql LIKE 'Bộ%' THEN 1
		WHEN a.dvql LIKE 'BCH%' THEN 2 
		WHEN a.dvql LIKE 'Công an%' THEN 3
		WHEN a.dvql LIKE 'Sở Tài nguyên%' THEN 4
		WHEN a.dvql LIKE 'Sở Giáo dục%' THEN 5
		WHEN a.dvql LIKE 'Sở Lao động%' THEN 6
		WHEN a.dvql LIKE 'Lực lượng%' THEN 7
		WHEN a.dvql LIKE 'Công ty%' THEN 8
		WHEN a.dvql LIKE 'Công viên%' THEN 9
		WHEN a.dvql LIKE 'Trung tâm%' THEN 10
		WHEN a.dvql LIKE 'Cảng%' THEN 11
		WHEN a.dvql LIKE 'Thành phố%' THEN 12
		WHEN a.dvql = 'Quận 1' THEN 13
		WHEN a.dvql = 'Quận 3' THEN 14
		WHEN a.dvql = 'Quận 4' THEN 15
		WHEN a.dvql = 'Quận 5' THEN 16
		WHEN a.dvql = 'Quận 6' THEN 17
		WHEN a.dvql = 'Quận 7' THEN 18
		WHEN a.dvql = 'Quận 8' THEN 19
		WHEN a.dvql = 'Quận 10' THEN 20
		WHEN a.dvql = 'Quận 11' THEN 21
		WHEN a.dvql = 'Quận 12' THEN 22
		WHEN a.dvql = 'Quận Gò Vấp' THEN 23
		WHEN a.dvql = 'Quận Phú Nhuận' THEN 24
		WHEN a.dvql = 'Quận Tân Bình' THEN 25
		WHEN a.dvql = 'Quận Tân Phú' THEN 26
		WHEN a.dvql = 'Quận Bình Tân' THEN 27
		WHEN a.dvql = 'Quận Bình Thạnh' THEN 28
		WHEN a.dvql = 'Huyện Bình Chánh' THEN 29
		WHEN a.dvql = 'Huyện Củ Chi' THEN 30
		WHEN a.dvql = 'Huyện Hóc Môn' THEN 31
		WHEN a.dvql = 'Huyện Nhà Bè' THEN 32
		WHEN a.dvql = 'Huyện Cần Giờ' THEN 33
		ElSE 34 END
	ASC
) AS donviquanly 
FROM PhuongTienHuyDong a
GROUP BY a.tenphuongtienttb, a.dvt
ORDER BY a.tenphuongtienttb ASC

-- thong ke thiet hai thien tai chi tiet
SELECT a.diadiem AS tenhuyen, a.loaithientai, a.doituongthiethai,a.motathiethai, SUM(a.soluong) AS Soluong, a.dvtthiethai, c.color AS mamau 
FROM thiethai_thientai a
LEFT JOIN Color c ON a.doituongthiethai = c.name
-- WHERE a.year = 2020
GROUP BY a.diadiem, a.loaithientai, a.doituongthiethai, a.motathiethai, a.dvtthiethai, c.color
ORDER BY a.diadiem ASC

-- thong ke thiet hai thien tai toan thanh pho
SELECT  a.doituongthiethai, SUM(a.soluong) AS soluong, c.color AS mamau, 'Đối tượng thiệt hại' AS phamvithongke  
FROM thiethai_thientai a
LEFT JOIN Color c ON a.doituongthiethai = c.name
GROUP BY  a.doituongthiethai, c.color
ORDER BY a.doituongthiethai ASC

SELECT json_agg(json_build_object('column',old2.key,'old',old2.value,'new',new2.value))
FROM 
(
	SELECT key, value FROM
	(SELECT to_json(t.*) as test FROM pets t) a,
	json_each_text(a.test)
) old2
JOIN 
(
	SELECT key, value FROM
	(SELECT to_json(t.*) as test FROM petsnew t) a,
	json_each_text(a.test)
) new2
ON old2.key = new2.key
WHERE old2.value <> new2.value;



