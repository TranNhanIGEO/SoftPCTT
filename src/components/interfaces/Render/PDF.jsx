import "./Render.css";
const PDF = (props) => {
  const { width, height, source } = props;

  return (
    <div className="pdf-render">
      <object
        type="application/pdf"
        aria-label="pdf"
        width={width}
        height={height}
        data={source}
      >
        <a href={source}>Nhấn để chuyển đến tệp PDF!</a>
      </object>
    </div>
  );
};

export default PDF;
