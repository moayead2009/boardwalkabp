import React from "react";
import useStateContext from "../../hooks/useStateContext";

export default function ProtectedRoute() {
  const { context } = useStateContext();
  console.log(context);

  return <div>ProtectedRoute</div>;
}
