import React from 'react'
import useStateContext from '../hooks/useStateContext'

export default function Categories() {
    const {context} = useStateContext()
    console.log(context)
    
  return (
    <div>
    <h1>Categories</h1>
    <p>Hello from categories {this.props.name} {this.props.children}</p>
    </div>
  )
}
