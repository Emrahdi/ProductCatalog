import * as React from 'react';
import { connect } from 'react-redux';
import { AntGrid, AntGridColumn } from './AntGrid';
import { config } from '../_helpers/config';
import { message  } from 'antd';

class ProductCatalogPage extends React.Component<any, any> {
    constructor(props: any) {
        super(props);
    }

    onSelectedRow = (selectedKeys: any) =>
    {
        message.info("Row is checked/unchecked!" + selectedKeys);
    }

    render() {
        const productColumns:AntGridColumn[]  = 
        [
            {
                key: 'Code',
                title: 'Product Code',
                dataIndex: 'code',
                editable: true
            }, 
            {
                key: 'Name',
                title: 'Product Name',
                dataIndex: 'name',
                editable: true
            }, 
            {
                key: 'Price',
                title: 'Price',
                dataIndex: 'price',
                editable: true,
                inputType: 'number',
            }, 
            {
                key: 'LastUpdatedDate',
                title: 'Last Updated Date',
                dataIndex: 'lastUpdatedDate',
                editable: false,
                inputType: 'date'
            }
        ];

        return (
                <div>
                <AntGrid searchActionUrl = {config.apiUrl + '/product/SearchProducts'} 
                        saveActionUrl = {config.apiUrl + '/product/SaveProduct'}
                        deleteActionUrl = {config.apiUrl + '/product/RemoveProduct'}
                        operationColumnTitle = "Operations"
                        columns = { productColumns }
                        title = "Products"
                        onSelectedRow = {this.onSelectedRow}
                        rowKey="code"
                        />
                </div>
        );
    } 
}

function mapStateToProps(state: any) {
    const { users, authentication } = state;
    const { user } = authentication;
    return {
        user,
        users
    };
}

const connectedProductCatalogPage = connect(mapStateToProps)(ProductCatalogPage);
export { connectedProductCatalogPage as ProductCatalogPage };