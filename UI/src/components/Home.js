import React from 'react'
import useStateContext from '../hooks/useStateContext'

export default function Home() {

  const {context} = useStateContext()
  console.log(context)


  return (
    <h1>Home</h1>
  )
}
