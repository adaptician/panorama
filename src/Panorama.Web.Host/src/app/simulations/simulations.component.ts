import {Component, OnInit} from '@angular/core';
import {IViewSceneDto, ViewSceneDto} from "@shared/service-proxies/scenography/dtos/ViewSceneDto";
import {AutoMapper} from "@shared/service-proxies/common/AutoMapper";

class Test implements IViewSceneDto {
    id: number = 1;
    name: string = "test";
    description: string = "test desc";
    scenographyId: number;
    sceneData: string | undefined;
    bogus: string = "bogus";
}

@Component({
    selector: 'sim-simulations',
    templateUrl: './simulations.component.html',
    styleUrl: './simulations.component.less'
})
export class SimulationsComponent implements OnInit {

    // TODO: move this onto component base class
    private _mapper: AutoMapper = new AutoMapper();
    
    ngOnInit(): void {
        debugger;

        const dto3 = new ViewSceneDto();
        const rest = this._mapper.map(new Test(), ViewSceneDto);
        
        
        const x = "";

        // const x = {
        //     "name": "test",
        //     "fluff": "no map"
        // };
        // const mapped = dto.fromJS(ViewSceneDto, x);
        // const jsonString = dto.fromJson(JSON.stringify(x));
        
        // console.log(`JSON STRING ${jsonString}`);
    }

}
