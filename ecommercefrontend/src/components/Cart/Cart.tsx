import React, {FC, useEffect} from 'react';
import {useTypesSelector} from "../../hooks/useTypedSelector";
import {useActions} from "../../hooks/useActions";
import styled from "styled-components";
import Product from "../ProductsList/Product";

const ProductContainer = styled.div`
  padding: 0 25px;
`;

const ProductTotalWrapper = styled.div`
  margin-left: auto;
  margin-top: auto;
`;

const ProductTotal = styled.span`
  display: flex;
  justify-content: center;
  background-color: #fff;
  padding: 20px;
  font-weight: bold;
  font-size: 22px;
`;

const ProductInner = styled.div`
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(350px, 400px));
  justify-content: center;
`;

const EmptyCartMessage = styled.p`
  font-size: 25px;
  text-align: center;
  font-weight: bold;
`;

interface ProductInterface {
    id: number;
    title: number;
    stock: number;
    price: number;
    images: string;
    quantity?: number;
}

const Cart: FC = () => {
    const selector = useTypesSelector(state => state.cartProducts)

    const {deleteProductFromCart, increaseProductQuantity, decreaseProductQuantity, getProductsTotal} = useActions()

    useEffect(() => {
        getProductsTotal()
    }, [selector.cartProducts])

    const handleRemoveFromCart = (id: any) => {
        deleteProductFromCart(id)
    }

    const incrementProductQuantity = (id: any) => {
        increaseProductQuantity(id)
    }

    const decrementProductQuantity = (id: any) => {
        decreaseProductQuantity(id)
    }

    return (
        <ProductContainer>
            <ProductTotalWrapper>
                <ProductTotal>Total: {selector.total}$</ProductTotal>
            </ProductTotalWrapper>
            <ProductInner>
                {selector.cartProducts?.length ? selector.cartProducts.map((el: any) => (
                    <React.Fragment key={el.gid}>
                        <Product id={el.gid}
                                 incrementProductQuantity={incrementProductQuantity}
                                 decrementProductQuantity={decrementProductQuantity}
                                 isShowQuantity={true}
                                 handleRemoveFromCart={handleRemoveFromCart}
                                 title={el.title}
                                 photo={el.photo}
                                 stock={el.stock}
                                 price={el.price}
                                 quantity={el.quantity}/>
                    </React.Fragment>
                    )) : <EmptyCartMessage>You have no saved items</EmptyCartMessage>}
            </ProductInner>
        </ProductContainer>
    );
};

export default Cart;