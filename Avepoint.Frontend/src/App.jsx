import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import CafePage from './pages/Cafe/CafePage';
import EmployeePage from './pages/Employee/EmployeePage';
import AddEditCafePage from './pages/Cafe/AddEditCafePage';
import AddEditEmployeePage from './pages/Employee/AddEditEmployeePage';

const App = () => (
  <Router>
    <Routes>
      <Route path="/cafes" element={<CafePage />} />
      <Route path="/employees" element={<EmployeePage />} />
      <Route path="/add-cafe" element={<AddEditCafePage />} />
      <Route path="/edit-cafe/:id" element={<AddEditCafePage />} />
      <Route path="/add-employee" element={<AddEditEmployeePage />} />
      <Route path="/edit-employee/:id" element={<AddEditEmployeePage />} />
    </Routes>
  </Router>
);

export default App;
