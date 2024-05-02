export const queryValidation = (whereClause) => {
  const firstColumn = whereClause["firstColumn"];
  const firstWhere = whereClause["firstWhere"];
  const secondColumn = whereClause["secondColumn"];
  const secondWhere = whereClause["secondWhere"];
  const fromValue = whereClause["fromValue"];
  const toValue = whereClause["toValue"];
  const startTime = whereClause["startTime"];
  const endTime = whereClause["endTime"];
  const onlyTime = whereClause["onlyTime"];
  const startTimeDef = "1970-01-01";
  const endTimeDef = new Date().toISOString().split("T")[0];
  const aFewMonth = whereClause["aFewMonth"]?.join(", ");
  const aYear = whereClause["aYear"];

  let query = "";

  // Both query - compare

  if (firstColumn && firstWhere && secondColumn && secondWhere) {

    if (firstColumn !== secondColumn) {
      query =
        `(a.${firstColumn} = '${firstWhere}')` +
        ` AND ` +
        `(a.${secondColumn} = '${secondWhere}')`;
    }

    if (firstColumn === secondColumn) {
      query =
        `(a.${firstColumn} = '${firstWhere}')` +
        ` OR ` +
        `(a.${secondColumn} = '${secondWhere}')`;
    }

  } else {

    if (firstColumn && firstWhere) {
      query = 
        `a.${firstColumn} = '${firstWhere}'`;
    }

    if (secondColumn && secondWhere) {
      query = 
        `a.${secondColumn} = '${secondWhere}'`;
    }
    
  }

  // Second query - range
  
  if (fromValue || toValue) {
    
    if (query?.length) {
      query += " AND ";
    }

  }

  if (secondColumn && fromValue && toValue) {

    query +=
      `(a.${secondColumn} >= '${fromValue}'` +
      ` AND ` +
      `a.${secondColumn} <= '${toValue}')`;

  } else {

    if (secondColumn && fromValue && !toValue) {
      query += 
        `(a.${secondColumn} >= '${fromValue}')`;
    }

    if (secondColumn && toValue && !fromValue) {
      query += 
        `(a.${secondColumn} <= '${toValue}')`;
    }

  }

  // Time query - compare
  
  if (startTime || endTime || onlyTime?.length || aFewMonth || aYear) {

    if (query?.length) {
      query += " AND ";
    }

  }

  if (startTime || endTime) {

    query +=
      `(a.NGAY BETWEEN '${startTime || startTimeDef}'` +
      ` AND ` +
      `'${endTime || endTimeDef}')`;

  }
  if (onlyTime) {

    const hasChar = onlyTime.split("-");
    if (hasChar?.length === 1) {
      query += 
        `(a.YEAR IN (${hasChar[0]}))`;
    }

    if (hasChar?.length === 2) {
      query +=
        `(a.YEAR IN (${hasChar[0]})` + 
        ` AND ` + 
        `a.MONTH IN (${hasChar[1]}))`;
    }

    if (hasChar?.length === 3) {
      query +=
        `(a.YEAR IN (${hasChar[0]})` +
        ` AND ` +
        `a.MONTH IN (${hasChar[1]})` +
        ` AND ` +
        `a.DAY IN (${hasChar[2]}))`;
    }

  }

  // Time query - range

  if (aFewMonth && aYear) {

    query += 
      `(a.MONTH IN (${aFewMonth})` + 
      ` AND ` + 
      `a.YEAR IN (${aYear}))`;

  } else {

    if (aFewMonth && !aYear) {
      query += 
        `(a.MONTH IN (${aFewMonth}))`;
    }

    if (aYear && !aFewMonth) {
      query += 
        `(a.YEAR IN (${aYear}))`;
    }
    
  }

  return query;
};
