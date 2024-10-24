import {RouterModule, Routes} from "@angular/router";
import {NgModule} from "@angular/core";
import {SimulationsComponent} from "./simulations.component";
import {AppRouteGuard} from "@shared/auth/auth-route-guard";

const routes: Routes = [
    {
        path: '',
        component: SimulationsComponent,
        pathMatch: 'full',
        data: { permission: 'Pages.Tenant.Simulations.View' },
        canActivate: [AppRouteGuard]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class SimulationsRoutingModule {}