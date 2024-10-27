﻿import { TreeNode } from 'primeng/api/treenode';

export class PanoTreeNode<T> implements TreeNode<T> {
    checked?: boolean;
    /**
     * Label of the node.
     */
    label?: string;
    /**
     * Data represented by the node.
     */
    data?: T;
    /**
     * Icon of the node to display next to content.
     */
    icon?: string;
    /**
     * Icon to use in expanded state.
     */
    expandedIcon?: string;
    /**
     * Icon to use in collapsed state.
     */
    collapsedIcon?: string;
    /**
     * An array of treenodes as children.
     */
    children?: TreeNode<any>[];
    /**
     * Specifies if the node has children. Used in lazy loading.
     * @defaultValue false
     */
    leaf?: boolean;
    /**
     * Expanded state of the node.
     * @defaultValue false
     */
    expanded?: boolean;
    /**
     * Type of the node to match a template.
     */
    type?: string;
    /**
     * Parent of the node.
     */
    parent?: TreeNode<T>;
    /**
     * Defines if value is partially selected.
     */
    partialSelected?: boolean;
    /**
     * Inline style of the node.
     */
    style?: any;
    /**
     * Style class of the node.
     */
    styleClass?: string;
    /**
     * Defines if the node is draggable.
     */
    draggable?: boolean;
    /**
     * Defines if the node is droppable.
     */
    droppable?: boolean;
    /**
     * Whether the node is selectable when selection mode is enabled.
     * @defaultValue false
     */
    selectable?: boolean;
    /**
     * Mandatory unique key of the node.
     */
    key?: string;
    /**
     * Defines if the node is loading.
     */
    loading?: boolean;
    
    constructor(data: Partial<PanoTreeNode<T>> = {}) {
        if (data) {
            for (let property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }
}