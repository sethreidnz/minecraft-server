// node_modules
import React from "react";
import { BrowserRouter as Router, Route } from "react-router-dom";
import "normalize.css";
import "./styles/global.scss";

// local components
import { Layout } from "./components/Layout";
import { Home } from "./pages/Home";

function App() {
  return (
    <Router>
      <Layout>
        <Route path="/" exact component={Home} />
      </Layout>
    </Router>
  );
}

export default App;
