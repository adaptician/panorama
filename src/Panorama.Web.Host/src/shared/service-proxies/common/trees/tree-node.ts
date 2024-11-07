import { TreeNode } from 'primeng/api/treenode';

export class TreeNodeBase<T> implements TreeNode<T> {
    // INTERFACE PROPERTIES
    checked?: boolean;
    label?: string;
    data?: any;
    icon?: string;
    expandedIcon?: string;
    collapsedIcon?: string;
    children?: TreeNode[];
    leaf?: boolean;
    expanded?: boolean;
    type?: string;
    parent?: TreeNode;
    partialSelected?: boolean;
    styleClass?: string;
    draggable?: boolean;
    droppable?: boolean;
    selectable?: boolean;
    key?: string;
    loading?: boolean;

    constructor(data: Partial<TreeNodeBase<T>> = {}) {
        if (data) {
            for (let property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }
}

export interface TreeNodeColumn {
    field: string;
    header: string;
}
