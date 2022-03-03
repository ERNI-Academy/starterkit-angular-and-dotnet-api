import { InputModule } from './../component/input/input.module';
import { OverlayModule } from '@angular/cdk/overlay';
import {NgModule} from '@angular/core';
// import {
//   MatAutocompleteModule,
//   MatBadgeModule,
//   MatBottomSheetModule,
//   MatButtonModule,
//   MatButtonToggleModule,
//   MatCardModule,
//   MatCheckboxModule,
//   MatChipsModule,
//   MatDatepickerModule,
//   MatDialogModule,
//   MatDividerModule,
//   MatExpansionModule,
//   MatFormFieldModule,
//   MatGridListModule,
//   MatIconModule,
//   MatInputModule,
//   MatListModule,
//   MatMenuModule,
//   MatNativeDateModule,
//   MatPaginatorModule,
//   MatProgressBarModule,
//   MatProgressSpinnerModule,
//   MatRadioModule,
//   MatRippleModule,
//   MatSelectModule,
//   MatSidenavModule,
//   MatSliderModule,
//   MatSlideToggleModule,
//   MatSnackBarModule,
//   MatSortModule,
//   MatStepperModule,
//   MatTableModule,
//   MatTabsModule,
//   MatToolbarModule,
//   MatTooltipModule,
//   MatTreeModule,
// } from '@angular/material';

import {MatCardModule} from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatMenuModule } from '@angular/material/menu';
import { MatInputModule } from '@angular/material/input';
import { MatTabsModule } from '@angular/material/tabs';
// import { MatAutocompleteModule } from '@angular/material/autocomplete';
// import { MatInputModule } from '@angular/material/input';



// import {A11yModule} from '@angular/cdk/a11y';
// import {BidiModule} from '@angular/cdk/bidi';
// import {ObserversModule} from '@angular/cdk/observers';
// import {OverlayModule} from '@angular/cdk/overlay';
// import {PlatformModule} from '@angular/cdk/platform';
// import {PortalModule} from '@angular/cdk/portal';
// import {CdkStepperModule} from '@angular/cdk/stepper';
// import {CdkTableModule} from '@angular/cdk/table';
// import {CdkTreeModule} from '@angular/cdk/tree';
// import {DragDropModule} from '@angular/cdk/drag-drop';

/**
 * NgModule that includes all Material modules.
 */
@NgModule({
  exports: [
    // CDK

    // A11yModule,
    // BidiModule,
    // ObserversModule,
    OverlayModule,
    // PlatformModule,
    // PortalModule,
    // CdkStepperModule,
    // CdkTableModule,
    // CdkTreeModule,
    // DragDropModule,

    // Material

    // MatAutocompleteModule,
    // MatBadgeModule,
    // MatBottomSheetModule,
    MatButtonModule,
    // MatButtonToggleModule,
    MatCardModule,
    // MatCheckboxModule,
    // MatChipsModule,
    // MatDatepickerModule,
    MatDialogModule,
    // MatDividerModule,
    // MatExpansionModule,
    MatFormFieldModule,
    // MatGridListModule,
    MatIconModule,
    MatInputModule,
    MatListModule,
    MatMenuModule,
    // MatNativeDateModule,
    MatPaginatorModule,
    // MatProgressBarModule,
    // MatProgressSpinnerModule,
    // MatRadioModule,
    // MatRippleModule,
    // MatSelectModule,
    MatSidenavModule,
    // MatSliderModule,
    // MatSlideToggleModule,
    MatSnackBarModule,
    MatSortModule,
    // MatStepperModule,
    MatTableModule,
    MatTabsModule,
    MatToolbarModule,
    // MatTooltipModule,
    // MatTreeModule,
  ]
})
export class MaterialModule {}
