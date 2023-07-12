import { Outlet, Navigate } from "react-router-dom";

export const AuthGuard = () => {
    const auth = localStorage.getItem("token");
    return auth !== null ? <Outlet /> : <Navigate to="" />
}