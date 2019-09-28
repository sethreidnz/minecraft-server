// node_modules
import React from "react";
import Navbar from 'react-bootstrap/Navbar';

// local imports
import "./Layout.scss";

export const Layout = ({ children }) => (
  <>
    <Navbar bg="light">
      <Navbar.Brand href="#home">Minecraft server</Navbar.Brand>
    </Navbar>
    <main>{children}</main>
    <footer />
  </>
);
