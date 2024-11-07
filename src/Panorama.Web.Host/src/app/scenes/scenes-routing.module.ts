import {RouterModule, Routes} from "@angular/router";
import {NgModule} from "@angular/core";
import {ScenesComponent} from "./scenes.component";
import {AppRouteGuard} from "@shared/auth/auth-route-guard";

const routes: Routes = [
    {
        path: '',
        component: ScenesComponent,
        pathMatch: 'full',
        data: { permission: 'Pages.Tenant.Scenes' },
        canActivate: [AppRouteGuard]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule],
})
export class ScenesRoutingModule {}