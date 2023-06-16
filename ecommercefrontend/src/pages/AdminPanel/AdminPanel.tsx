import React, { useState } from 'react';
import axios from "axios";

const AdminPanel = () => {
    const [productData, setProductData] = useState({
        title: '',
        description: '',
        photo: '',
        price: 0,
        quantity: 1,
        stock: 0,
        delivery: true,
        userGid: '6adee3a8-6388-4edc-8778-4a417efb0899',
    });

    const [productDataUpdate, setProductDataUpdate] = useState({
        title: '',
        gid: '',
        description: '',
        photo: '',
        price: 0,
        quantity: 1,
        stock: 0,
        delivery: true,
        userGid: '6adee3a8-6388-4edc-8778-4a417efb0899',
    });

    const [productId, setProductId] = useState('');

    const handleChange = (e: any) => {
        const { name, value } = e.target;
        setProductData((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    };

    const handleChangeUpdate = (e: any) => {
        const { name, value } = e.target;
        setProductDataUpdate((prevData) => ({
            ...prevData,
            [name]: value,
        }));
    };

    const handleGetId = (e: any) => {
        setProductId(e.target.value)
    }

    const handleDeleteSubmit = (e: any) => {
        e.preventDefault()
        axios.delete("http://localhost:5000/api/products?gid=" + productId)
            .then(res => {
                console.log(res)
            })
            .catch(error => {
                console.log(error)
            });
    }

    const handleUpdate = (e: any) => {
        e.preventDefault()
        const patchData = [
            { op: 'replace', path: '/title', value: productDataUpdate.title },
            { op: 'replace', path: '/description', value: productDataUpdate.description },
            { op: 'replace', path: '/photo', value: productDataUpdate.photo },
            { op: 'replace', path: '/price', value: productDataUpdate.price },
            { op: 'replace', path: '/quantity', value: productDataUpdate.quantity },
            { op: 'replace', path: '/stock', value: productDataUpdate.stock },
            { op: 'replace', path: '/delivery', value: productDataUpdate.delivery },
            { op: 'replace', path: '/userGid', value: productDataUpdate.userGid },
        ];

        axios
            .patch(`http://localhost:5000/api/products/${productDataUpdate.gid}`, patchData, {
                headers: {
                    'Content-Type': 'application/json-patch+json',
                },
            })
            .then((response) => {
                console.log(response.data);
            })
            .catch((error) => {
                console.error(error);
            });
    }

    const handleSubmit = (e: any) => {
        e.preventDefault();

        const config = {
            headers: {
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
            },
        };

        axios.post("http://localhost:5000/api/products", productData, config)
            .then(res => {
                console.log(res)
            })
            .catch(error => {
                console.log(error)
            });
    };

    return (
        <div className="forms-wrapper">
            <div>
                <h2>Create Product</h2>
                <form className="create-product" onSubmit={handleSubmit}>
                    <div>
                        <label htmlFor="title">Title:</label>
                        <input
                            type="text"
                            id="title"
                            name="title"
                            value={productData.title}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div>
                        <label htmlFor="description">Description:</label>
                        <input
                            id="description"
                            name="description"
                            value={productData.description}
                            onChange={handleChange}
                            required
                        ></input>
                    </div>
                    <div>
                        <label htmlFor="photo">Photo:</label>
                        <input
                            type="text"
                            id="photo"
                            name="photo"
                            value={productData.photo}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div>
                        <label htmlFor="price">Price:</label>
                        <input
                            type="number"
                            id="price"
                            name="price"
                            value={productData.price}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div>
                        <label htmlFor="stock">Stock:</label>
                        <input
                            type="number"
                            id="stock"
                            name="stock"
                            value={productData.stock}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <button type="submit">Create Product</button>
                </form>
            </div>
            <div>
                <h2>Delete Product</h2>
                <form onSubmit={handleDeleteSubmit}>
                    <div>
                        <label htmlFor="stock">Id:</label>
                        <input
                            type="string"
                            id="id"
                            name="id"
                            value={productId}
                            onChange={(e) => handleGetId(e)}
                            required
                        />
                    </div>
                    <button type="submit">Delete Product</button>
                </form>
            </div>
            <div>
                <h2>Update Product</h2>
                <form onSubmit={handleUpdate}>
                    <label htmlFor="gid">Guid:</label>
                    <input
                        type="text"
                        id="gid"
                        name="gid"
                        value={productDataUpdate.gid}
                        onChange={handleChangeUpdate}
                        required
                    />
                    <label htmlFor="title">Title:</label>
                    <input
                        type="text"
                        id="title"
                        name="title"
                        value={productDataUpdate.title}
                        onChange={handleChangeUpdate}
                    />
                    <label htmlFor="description">Description:</label>
                    <input
                        type="text"
                        id="description"
                        name="description"
                        value={productDataUpdate.description}
                        onChange={handleChangeUpdate}
                    />
                    <label htmlFor="photo">Photo:</label>
                    <input
                        type="text"
                        id="photo"
                        name="photo"
                        value={productDataUpdate.photo}
                        onChange={handleChangeUpdate}
                    />
                    <label htmlFor="price">Price:</label>
                    <input
                        type="number"
                        id="price"
                        name="price"
                        value={productDataUpdate.price}
                        onChange={handleChangeUpdate}
                    />
                    <label htmlFor="stock">Stock:</label>
                    <input
                        type="number"
                        id="stock"
                        name="stock"
                        value={productDataUpdate.stock}
                        onChange={handleChangeUpdate}
                    />
                    <button type="submit">Update Product</button>
                </form>
            </div>
        </div>
    );
};

export default AdminPanel;
