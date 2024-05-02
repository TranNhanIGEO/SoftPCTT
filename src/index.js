import React from "react";
import ReactDOM from "react-dom/client";
import App from "./App";
import { Provider } from "react-redux";
import { PersistGate } from "redux-persist/integration/react";
import { configStore, persistor } from "./stores/configStore";
import GlobalStyle from "src/components/GlobalStyle";

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(
  // <React.StrictMode>
  <Provider store={configStore}>
    <PersistGate loading={null} persistor={persistor}>
      <GlobalStyle>
        <App />
      </GlobalStyle>
    </PersistGate>
  </Provider>
  // </React.StrictMode>
);
