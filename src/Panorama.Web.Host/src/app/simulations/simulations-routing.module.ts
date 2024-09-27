import {RouterModule, Routes} from "@angular/router";
import {NgModule} from "@angular/core";
import {SimulationsComponent} from "./simulations.component";
import {AppRouteGuard} from "@shared/auth/auth-route-guard";
import {ScenesComponent} from "@app/simulations/scenes/scenes.component";

const routes: Routes = [
    {
        path: '',
        component: SimulationsComponent,
        pathMatch: 'full',
        data: { permission: 'Pages.Tenant.Simulations' },
        canActivate: [AppRouteGuard]
    },
    { path: 'scenes', component: ScenesComponent, data: { permission: 'Pages.Tenant.Simulations' }, canActivate: [AppRouteGuard] }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class SimulationsRoutingModule {}