import React, {FC, useEffect} from 'react';
import {useTypesSelector} from "../../hooks/useTypedSelector";
import {useActions} from "../../hooks/useActions";
import styled from "styled-components";
import Product from "./Product";
import Loading from "../Loading/Loading";

const ProductContainer = styled.div`
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(350px, 1fr));
  justify-content: center;
  align-items: center;
  padding: 0 25px;
  
  @media (max-width: 749px){
    grid-template-columns: repeat(auto-fit, minmax(350px, 400px));
  }
`;

interface ProductInterface {
    id: number;
    title: number;
    stock: number;
    price: number;
    // images: string;
    photo: string;
    quantity?: number;
}

const ProductsList: FC = () => {
    const {products, error, loading} = useTypesSelector(state => state.products)
    const selector = useTypesSelector(state => state.cartProducts)
    const {fetchProducts, saveProductToCart} = useActions()

    useEffect(() => {
        fetchProducts()
    }, [])

    if(loading) {
        return <Loading />
    }

    if(error) {
        return <h1>{error}</h1>
    }

    const isFound = (id: any) => selector.cartProducts.some(element => {
        return element.gid === id;
    });

    const handleAddToCart = (product: ProductInterface) => {
        console.log(product)
        if(!isFound(product.id)) {
            saveProductToCart(product);
        }

        const items = JSON.parse(localStorage.getItem("product") || "[]");

        if(items.includes(product)) return

        items.push(product);

        localStorage.setItem("product", JSON.stringify(items));
    }

    return (
        <ProductContainer>
            {products?.length && products.map((el: any) => (
                <React.Fragment key={el.gid}>
                    <Product
                        id={el.gid}
                        title={el.title}
                        stock={el.stock}
                        photo={el.photo}
                        price={el.price}
                        product={el}
                        isHideDeleteButton={true}
                        handleAddToCart={handleAddToCart}/>
                </React.Fragment>
            ))}
        </ProductContainer>
    );
};

export default ProductsList;