const operators = [
  {
    col: "operator-col-1",
    operators: [
      { id: "equal", operator: " = " },
      { id: "greater_than", operator: " > " },
      { id: "less_than", operator: " < " },
      { id: "concat", operator: " % " },
      { id: "is", operator: " IS " },
    ],
  },
  {
    col: "operator-col-2",
    operators: [
      { id: "difference", operator: " <> " },
      { id: "greater_than_or_equal", operator: " >= " },
      { id: "less_than_or_equal", operator: " <= " },
      { id: "round_brackets", operator: " () " },
      { id: "in", operator: " IN " },
    ],
  },
  {
    col: "operator-col-3",
    operators: [
      { id: "like", operator: " LIKE " },
      { id: "and", operator: " AND " },
      { id: "or", operator: " OR " },
      { id: "not", operator: " NOT " },
      { id: "nul", operator: " NULL " },
    ],
  },
];

export default operators;
