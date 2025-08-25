import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Login from "./pages/Login";
import Home from "./pages/Home";
import Layout from "./pages/Layout"
import UserConfig from "./pages/UserConfig"

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route element={<Layout />}>
          <Route path="/home" element={<Home />} />
          <Route path="/user-configuration" element={<UserConfig />} />
        </Route>
      </Routes>
    </Router>
  );
}

export default App;
