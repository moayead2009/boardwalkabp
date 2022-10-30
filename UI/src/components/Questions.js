import React from 'react'
import useStateContext from '../hooks/useStateContext'

export default function Questions() {
    const {context} = useStateContext()
    console.log(context)
    
  return (
    <div>Questions</div>
  )
}
