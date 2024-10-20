import {Injector, ElementRef, OnDestroy, Component} from '@angular/core';
import { AppConsts } from '@shared/AppConsts';
import {
    LocalizationService,
    PermissionCheckerService,
    FeatureCheckerService,
    NotifyService,
    SettingService,
    MessageService,
    AbpMultiTenancyService
} from 'abp-ng2-module';

import { AppSessionService } from '@shared/session/app-session.service';
import {AutoMapper} from "@shared/service-proxies/common/AutoMapper";

interface AbpEventSubscription {
    eventName: string;
    callback: (...args: any[]) => void;
}

@Component({
    template: '',
})
export abstract class AppComponentBase implements OnDestroy {

    localizationSourceName = AppConsts.localization.defaultLocalizationSourceName;

    localization: LocalizationService;
    permission: PermissionCheckerService;
    feature: FeatureCheckerService;
    notify: NotifyService;
    setting: SettingService;
    message: MessageService;
    multiTenancy: AbpMultiTenancyService;
    appSession: AppSessionService;
    // Commented out because it breaks signalR services that inherit from base - nothing visibly breaks.
    // elementRef: ElementRef;

    eventSubscriptions: AbpEventSubscription[] = [];

    lastBusy: Date = new Date();
    busy: {
        loading: boolean,
        saving: boolean,
    } = <any>{};

    protected setBusy(key: string, isBusy: boolean) {
        this.lastBusy = new Date();
        this.busy[key] = isBusy;
    }
    
    protected resetBusy(): void {
        for (let key in Object.keys(this.busy)) {
            this.busy[key] = false;
        }
    }
    
    protected mapper: AutoMapper;

    constructor(injector: Injector) {
        this.localization = injector.get(LocalizationService);
        this.permission = injector.get(PermissionCheckerService);
        this.feature = injector.get(FeatureCheckerService);
        this.notify = injector.get(NotifyService);
        this.setting = injector.get(SettingService);
        this.message = injector.get(MessageService);
        this.multiTenancy = injector.get(AbpMultiTenancyService);
        this.appSession = injector.get(AppSessionService);
        // this.elementRef = injector.get(ElementRef);
        
        this.mapper = new AutoMapper();
    }

    ngOnDestroy(): void {
        this.unSubscribeAllEvents();
    }

    l(key: string, ...args: any[]): string {
        let localizedText = this.localization.localize(key, this.localizationSourceName);

        if (!localizedText) {
            localizedText = key;
        }

        if (!args || !args.length) {
            return localizedText;
        }

        args.unshift(localizedText);
        return abp.utils.formatString.apply(this, args);
    }

    isGranted(permissionName: string): boolean {
        return this.permission.isGranted(permissionName);
    }

    protected subscribeToEvent(eventName: string, callback: (...args: any[]) => void): void {
        abp.event.on(eventName, callback);
        this.eventSubscriptions.push({
            eventName,
            callback,
        });
    }

    private unSubscribeAllEvents() {
        this.eventSubscriptions.forEach((s) => abp.event.off(s.eventName, s.callback));
        this.eventSubscriptions = [];
    }
}
