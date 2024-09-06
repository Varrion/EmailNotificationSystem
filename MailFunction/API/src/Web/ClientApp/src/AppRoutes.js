import {Home} from "./components/Home";
import AddClientPage from "./pages/AddClientPage";
import AddTemplatePage from "./pages/AddTemplatePage";
import AdminPage from "./pages/AdminPage";

const AppRoutes = [
  {
    index: true,
    element: <Home/>
  },
  {
    path: '/admin-panel',
    element: <AdminPage/>
  },
  {
    path: '/add-client',
    element: <AddClientPage/>
  },
  {
    path: '/add-template',
    element: <AddTemplatePage/>
  },
];

export default AppRoutes;
