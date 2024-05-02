import { useState, useEffect } from "react";

const useScreen = () => {
  const [screenWidth, setScreenWidth] = useState(window.innerWidth);

  const handleResize = () => {
    setScreenWidth(window.innerWidth);
  };

  useEffect(() => {
    window.addEventListener("resize", handleResize);
    return () => {
      window.removeEventListener("resize", handleResize);
    };
  }, []);

  const screenSize = {
    screenWidth: screenWidth,
    isMobile: () => {
      return screenWidth <= 1024;
    },
    isDesktop: () => {
      return screenWidth > 1024;
    },
  }

  return screenSize;
};

export default useScreen;
