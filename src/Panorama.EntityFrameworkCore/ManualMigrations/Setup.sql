-- In some cases there is no UI for ABP entity management.
-- Where that is the case, this script should be run during initial setup to bridge the gap.

SELECT * FROM AbpFeatures

-- There is no UI in ABP for feature toggles - add this one manually.
INSERT INTO AbpFeatures
    (CreationTime, CreatorUserId, Discriminator, Name, Value, TenantId)
VALUES
(
    '2024-09-25 11:00:00.0000000',
    1,
    'TenantFeatureSetting',
    'Panorama.Simulations',
    'True',
    1
)