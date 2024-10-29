import {TreeNodeBase, TreeNodeColumn} from "./tree-node";
import {ViewSimulationDto, ViewSimulationRunDto} from "../../service-proxies";

export class SimulationTreeNode extends TreeNodeBase<ViewSimulationDto> {
    id?: number;
    children?: TreeNodeBase<ViewSimulationRunDto>[];

    constructor(data: ViewSimulationDto) {
        super();

        if (data) {
            // base
            this.label = data.name;
            this.data = data;
            this.leaf = false;

            // concrete
            this.id = data.id;
        }
    }
}

export class SimulationRunTreeNode extends TreeNodeBase<ViewSimulationRunDto> {
    id?: number;
    children?: null;

    constructor(data: ViewSimulationRunDto) {
        super();

        if (data) {
            // base
            this.label = data.startTime.toString();
            this.data = data;
            this.leaf = true;

            // concrete
            this.id = data.id;
        }
    }
}