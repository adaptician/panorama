import {NgModule} from "@angular/core";

import * as ApiServiceProxies from './scenography.service-proxies';

@NgModule({
    providers: [
        ApiServiceProxies.SceneServiceProxy
    ]
})
export class ScenographyServiceProxyModule { }