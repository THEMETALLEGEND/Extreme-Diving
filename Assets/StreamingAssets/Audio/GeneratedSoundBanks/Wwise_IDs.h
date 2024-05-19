/////////////////////////////////////////////////////////////////////////////////////////////////////
//
// Audiokinetic Wwise generated include file. Do not edit.
//
/////////////////////////////////////////////////////////////////////////////////////////////////////

#ifndef __WWISE_IDS_H__
#define __WWISE_IDS_H__

#include <AK/SoundEngine/Common/AkTypes.h>

namespace AK
{
    namespace EVENTS
    {
        static const AkUniqueID ATTACK_PISTOL = 2791278421U;
        static const AkUniqueID ENTER_WATER = 4125161945U;
        static const AkUniqueID EXIT_WATER = 387843989U;
        static const AkUniqueID FISH_COLLECT = 1348106800U;
        static const AkUniqueID FISH_SWIM = 1699223174U;
        static const AkUniqueID PAUSE_ALL = 3864097025U;
        static const AkUniqueID PLAYER_HURT = 1068092414U;
        static const AkUniqueID PLAYER_RELOAD = 1650679582U;
        static const AkUniqueID PURCHASE = 3809213376U;
        static const AkUniqueID RESUME_ALL = 3679762312U;
        static const AkUniqueID SHARK_ATTACK = 1677670675U;
        static const AkUniqueID SWORDFISH_ATTACK = 790547833U;
    } // namespace EVENTS

    namespace STATES
    {
        namespace MUSIC
        {
            static const AkUniqueID GROUP = 3991942870U;

            namespace STATE
            {
                static const AkUniqueID GAMEPLAY = 89505537U;
                static const AkUniqueID MENU = 2607556080U;
                static const AkUniqueID NONE = 748895195U;
            } // namespace STATE
        } // namespace MUSIC

        namespace WATER
        {
            static const AkUniqueID GROUP = 2654748154U;

            namespace STATE
            {
                static const AkUniqueID NONE = 748895195U;
                static const AkUniqueID PAUSED = 319258907U;
                static const AkUniqueID SURFACE = 1834394558U;
                static const AkUniqueID UNDERWATER = 2213237662U;
            } // namespace STATE
        } // namespace WATER

    } // namespace STATES

    namespace SWITCHES
    {
        namespace DEPTH
        {
            static const AkUniqueID GROUP = 681025064U;

            namespace SWITCH
            {
                static const AkUniqueID ABYSS = 4215188899U;
                static const AkUniqueID DEEP = 1976939195U;
                static const AkUniqueID SHALLOW = 2943574119U;
                static const AkUniqueID SURFACE = 1834394558U;
            } // namespace SWITCH
        } // namespace DEPTH

    } // namespace SWITCHES

    namespace GAME_PARAMETERS
    {
        static const AkUniqueID O2_METER = 2744448096U;
    } // namespace GAME_PARAMETERS

    namespace BANKS
    {
        static const AkUniqueID INIT = 1355168291U;
        static const AkUniqueID MAIN = 3161908922U;
    } // namespace BANKS

    namespace BUSSES
    {
        static const AkUniqueID ENEMY = 2299321487U;
        static const AkUniqueID ENVIRONMENT = 1229948536U;
        static const AkUniqueID MASTER_AUDIO_BUS = 3803692087U;
        static const AkUniqueID MENU = 2607556080U;
        static const AkUniqueID MUSIC = 3991942870U;
        static const AkUniqueID PLAYER = 1069431850U;
        static const AkUniqueID SFX = 393239870U;
    } // namespace BUSSES

    namespace AUDIO_DEVICES
    {
        static const AkUniqueID NO_OUTPUT = 2317455096U;
        static const AkUniqueID SYSTEM = 3859886410U;
    } // namespace AUDIO_DEVICES

}// namespace AK

#endif // __WWISE_IDS_H__
