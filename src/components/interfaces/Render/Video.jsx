import "./Render.css";
const Video = (props) => {
  const { width, height, source } = props;

  return (
    <div className="video-render">
      {source ? (
        <video
          width={width}
          height={height}
          controls
          src={source}
        />
      ): (
        "Loading..."
      )}
    </div>
  );
};

export default Video;
