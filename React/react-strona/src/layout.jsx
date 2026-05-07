import { Outlet, Link } from "react-router-dom";

function Layout() {
  return (
    <>
      <nav>
        <Link to="/">Home</Link>
        <Link to="/Login">Login</Link>
        <Link to="/Register">Register</Link>
      </nav>

      <main>
        <Outlet />
      </main>
    </>
  );
}

export default Layout;