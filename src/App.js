import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { Protected, Allowed, Required } from "./middleware/Router";
import { protectedRoutes, allowedRoutes, errorRoutes } from "./routes";
import RootLayout from "./layouts/RootLayout";
import { ToastProvider } from "./contexts/Toast";
import ToastContainer from "./components/interfaces/Toast";
import Browser from "./middleware/Browser";

function App() {
  return (
    <div className="App">
      <Router>
        <ToastProvider>
          <ToastContainer />
          <Browser>
            <Routes>
              <Route element={<Protected />}>
                {protectedRoutes.map((route) => (
                  <Route
                    key={route.path}
                    element={<Required role={route.role} />}
                  >
                    <Route
                      path={route.path}
                      element={<RootLayout>{route.component}</RootLayout>}
                    />
                  </Route>
                ))}
              </Route>
              <Route element={<Allowed />}>
                {allowedRoutes.map((route) => (
                  <Route
                    key={route.path}
                    path={route.path}
                    element={<RootLayout>{route.component}</RootLayout>}
                  />
                ))}
              </Route>
              <Route path={errorRoutes.path} element={errorRoutes.component} />
            </Routes>
          </Browser>
        </ToastProvider>
      </Router>
    </div>
  );
}

export default App;
