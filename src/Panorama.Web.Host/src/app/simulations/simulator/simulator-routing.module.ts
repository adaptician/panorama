import {RouterModule, Routes} from "@angular/router";
import {NgModule} from "@angular/core";
import {SimulatorComponent} from "./simulator.component";
import {AppRouteGuard} from "../../../shared/auth/auth-route-guard";

const routes: Routes = [
    {
        path: '',
        component: SimulatorComponent,
        pathMatch: 'full',
        data: { permission: 'Pages.Tenant.Simulations.Running.Participate' },
        canActivate: [AppRouteGuard]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class SimulatorRoutingModule {}